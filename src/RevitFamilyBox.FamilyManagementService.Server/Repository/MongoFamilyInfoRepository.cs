using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MongoDB.Driver;
using RevitFamilyBox.FamilyManagementService.Server.Config;
using RevitFamilyBox.FamilyManagementService.Server.DAL.Models;
using RevitFamilyBox.FamilyManagementService.Server.Domain;
namespace RevitFamilyBox.FamilyManagementService.Server.Repository
{
    public class MongoFamilyInfoRepository: IFamilyInfoRepository
    {
        private readonly FamilyManagerConfig _familyMgrCfg;


        public MongoFamilyInfoRepository(FamilyManagerConfig familyMgrCfg)
        {
            _familyMgrCfg = familyMgrCfg;
        }


        public async Task<IEnumerable<FamilyInfo>> AddFamilyInfosAsync(IEnumerable<FamilyInfo> familyInfos)
        {
            var client = new MongoClient(_familyMgrCfg.DBConnection);
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

        public async Task<IEnumerable<FamilyInfo>> FindFamilyInfosAsync(int userId, int versionId, string xPath = null)
        {
            var client = new MongoClient(_familyMgrCfg.DBConnection);
            IMongoDatabase db = client.GetDatabase(_familyMgrCfg.DBName);

            FilterDefinitionBuilder<FamilyInfoEntity> builderFilter = Builders<FamilyInfoEntity>.Filter;
            FilterDefinition<FamilyInfoEntity> filter =
              builderFilter.And(builderFilter.Eq("UserId", userId),
                builderFilter.Eq("VersionId", versionId));

            IMongoCollection<FamilyInfoEntity> collection =
              db.GetCollection<FamilyInfoEntity>(_familyMgrCfg.DBCollection);
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
            return new FamilyInfoEntity() { Id = new MongoDB.Bson.ObjectId(familyInfo.Id), Name = familyInfo.Name, FileBlobId = familyInfo.FileBlobId, Parameters = ToJsonString(familyInfo.Parameters), UserId = familyInfo.UserId, VersionId = familyInfo.VersionId };
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

    }
}
