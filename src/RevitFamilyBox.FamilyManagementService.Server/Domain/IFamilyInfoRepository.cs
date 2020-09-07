using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevitFamilyBox.FamilyManagementService.Server.Domain
{
    public interface IFamilyInfoRepository
    {
        Task<IEnumerable<FamilyInfo>> AddFamilyInfosAsync(IEnumerable<FamilyInfo> familyInfos);

        Task<IEnumerable<FamilyInfo>> FindFamilyInfosAsync(int userId, int versionId, string xPath = null);

        Task<bool> RemoveFamilyInfosAsync(IEnumerable<int> familyIds, int versionId);

        Task<bool> UpdateFamilyInfosAsync(IEnumerable<FamilyInfo> familyInfos);
    }
}
