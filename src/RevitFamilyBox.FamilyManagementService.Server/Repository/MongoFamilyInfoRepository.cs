using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MongoDB.Driver;
using RevitFamilyBox.FamilyManagementService.Server.Config;
using RevitFamilyBox.FamilyManagementService.Server.Domain;
namespace RevitFamilyBox.FamilyManagementService.Server.Repository
{
    public class MongoFamilyInfoRepository: IFamilyInfoRepository
    {
        private readonly FamilyManagerConfig _familyMgrCfg;
        private readonly Lazy<MongoClient> _mongoClient;


        public MongoFamilyInfoRepository(FamilyManagerConfig familyMgrCfg)
        {
            _familyMgrCfg = familyMgrCfg;
            _mongoClient = new Lazy<MongoClient>(() => new MongoClient(familyMgrCfg.DBCollection));
        }


        public async Task<IEnumerable<FamilyInfo>> AddFamilyInfosAsync(IEnumerable<FamilyInfo> familyInfos)
        {
            var client = _mongoClient.Value;
            IMongoDatabase db = client.GetDatabase(_familyMgrCfg.DBName);

            IMongoCollection<FamilyInfoEntity> collection = db.GetCollection<FamilyInfoEntity>(_familyMgrCfg.DBCollection);
            var entities = new List<FamilyInfoEntity>();
            foreach (var item in familyInfos)
            {
                entities.Add(ToEntity(item));
            }

            await collection.InsertManyAsync(entities);

            return familyInfos;
        }

        public async Task<IEnumerable<FamilyInfo>> FindFamilyInfosAsync(int userId, string idPath = null)
        {
            var client = _mongoClient.Value;
            IMongoDatabase db = client.GetDatabase(_familyMgrCfg.DBName);
            IMongoCollection<FamilyInfoEntity> collection = db.GetCollection<FamilyInfoEntity>(_familyMgrCfg.DBCollection);

            var builderFilter = Builders<FamilyInfoEntity>.Filter;
            var userIdFilter = builderFilter.Eq("UserId", userId);
            FilterDefinition<FamilyInfoEntity> pathFilter = builderFilter.Empty;
            if (!string.IsNullOrWhiteSpace(idPath))
            {
                var splitedPath = idPath.Split('/');
                pathFilter = builderFilter.AnyIn("Parents", splitedPath);
            }

            var filter = builderFilter.And(userIdFilter, pathFilter);
            var queryResult = await collection.FindAsync(filter);
            var arrFamilyInfo = await queryResult.ToListAsync();
            var res = new List<FamilyInfo>();
            foreach (var item in arrFamilyInfo)
            {
                res.Add(ToInfo(item));
            }

            return res;
        }

        public async Task<bool> RemoveFamilyInfosAsync(IEnumerable<int> familyIds, int versionId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateFamilyInfosAsync(IEnumerable<FamilyInfo> familyInfos)
        {
            throw new NotImplementedException();
        }

        private FamilyInfoEntity ToEntity(FamilyInfo familyInfo)
        {
            return new FamilyInfoEntity() { Id = familyInfo.Id, EntityId = new MongoDB.Bson.ObjectId($"{familyInfo.Id}:{familyInfo.VersionId}"),  Name = familyInfo.Name, FileBlobId = familyInfo.FileBlobId, Parameters = ToJsonString(familyInfo.Parameters), UserId = familyInfo.UserId, VersionId = familyInfo.VersionId };
        }

        private FamilyInfo ToInfo(FamilyInfoEntity familyInfoEntity)
        {
            return new FamilyInfo() { Id = familyInfoEntity.Id.ToString(), Name = familyInfoEntity.Name, Parameters = JsonDocument.Parse(familyInfoEntity.Parameters), FileBlobId = familyInfoEntity.FileBlobId, UserId = familyInfoEntity.UserId, VersionId = familyInfoEntity.VersionId };
        }

        private static string ToJsonString(JsonDocument jdoc)
        {
            using (var stream = new MemoryStream())
            {
                Utf8JsonWriter writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
                jdoc.WriteTo(writer);
                writer.Flush();
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        public async Task<FamilyInfo> FindFamilyInfoAsync(string id, int versionId)
        {
            var client = _mongoClient.Value;
            IMongoDatabase db = client.GetDatabase(_familyMgrCfg.DBName);
            IMongoCollection<FamilyInfoEntity> collection = db.GetCollection<FamilyInfoEntity>(_familyMgrCfg.DBCollection);
            var res = await (await collection.FindAsync(k => k.EntityId == new MongoDB.Bson.ObjectId($"{id}:{versionId}"))).FirstOrDefaultAsync();
            if (res == null)
            {
                return null;
            }

            return ToInfo(res);
        }
    }
}
