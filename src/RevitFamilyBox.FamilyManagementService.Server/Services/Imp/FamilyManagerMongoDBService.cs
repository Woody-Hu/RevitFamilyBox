using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RevitFamilyBox.FamilyManagementService.Server.Config;
using RevitFamilyBox.FamilyManagementService.Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevitFamilyBox.FamilyManagementService.Server.Services.Imp
{
  public class FamilyManagerMongoDBService : IFamilyManagerService
  {
    private readonly FamilyManagerConfig _familyMgrCfg;


    public FamilyManagerMongoDBService(
      FamilyManagerConfig familyMgrCfg)
    {
      _familyMgrCfg = familyMgrCfg;
    }


    public async Task<IEnumerable<FamilyInfo>> AddFamilyInfoAsync(IEnumerable<FamilyInfo> familyInfos)
    {
      var client = new MongoClient(_familyMgrCfg.DBConnection);
      IMongoDatabase db = client.GetDatabase(_familyMgrCfg.DBName);

      IMongoCollection<FamilyInfo> collection =
        db.GetCollection<FamilyInfo>(_familyMgrCfg.DBCollection);

      await collection.InsertManyAsync(familyInfos);

      return familyInfos;
    }

    public async Task<IEnumerable<FamilyInfo>> GetFamilyInfoAsync(int userId, int versionId, string xPath = null)
    {
      var client = new MongoClient(_familyMgrCfg.DBConnection);
      IMongoDatabase db = client.GetDatabase(_familyMgrCfg.DBName);

      FilterDefinitionBuilder<FamilyInfo> builderFilter = Builders<FamilyInfo>.Filter;
      FilterDefinition<FamilyInfo> filter = 
        builderFilter.And(builderFilter.Eq("UserId", userId), 
          builderFilter.Eq("VersionId", versionId));

      IMongoCollection<FamilyInfo> collection = 
        db.GetCollection<FamilyInfo>(_familyMgrCfg.DBCollection);
      var queryResult = await collection.FindAsync(filter);
      var arrFamilyInfo = await queryResult.ToListAsync();

      return arrFamilyInfo;
    }

    public async Task<bool> RemoveFamilyInfoAsync(IEnumerable<int> familyIds, int versionId)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> UpdateFamilyInfoAsync(IEnumerable<FamilyInfo> familyInfos)
    {
      throw new NotImplementedException();
    }
  }
}
