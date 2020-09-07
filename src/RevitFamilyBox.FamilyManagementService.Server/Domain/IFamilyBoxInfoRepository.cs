using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevitFamilyBox.FamilyManagementService.Server.Domain
{
    public interface IFamilyBoxInfoRepository
    {
        Task<FamilyBoxInfo> FindFamilyBoxInfoByIdAsync(string id);

        // todo describe query
        Task<FamilyBoxInfo> FindFamilyBoxInfosAsync();

        Task<FamilyBoxInfo> CreateFamilyBoxInfoAsync(FamilyBoxInfo familyBoxInfo);

        Task<IEnumerable<FamilyBoxInfo>> CreateFamilyBoxInfosAsync(IEnumerable<FamilyBoxInfo> familyBoxInfos);

        Task<FamilyBoxInfo> UpdateFamilyBoxInfoAsync(FamilyBoxInfo familyBoxInfo);

        Task<IEnumerable<FamilyBoxInfo>> UpdateFamilyBoxInfosAsync(IEnumerable<FamilyBoxInfo> familyBoxInfos);

        Task<FamilyBoxInfo> DeleteFamilyBoxInfoAsync(string id);

        Task<IEnumerable<FamilyBoxInfo>> DeleteFamilyBoxInfosAsync(IEnumerable<string> ids);
    }
}
