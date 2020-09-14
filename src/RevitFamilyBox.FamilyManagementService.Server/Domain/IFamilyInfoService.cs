using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevitFamilyBox.FamilyManagementService.Server.Domain
{
    public interface IFamilyInfoService
    {
        Task<FamilyInfo> GetFamilyInfoLastVersionAsync(string familyInfoId);

        Task<FamilyInfo> GetFamilyInfoVersionAsync(string familyInfoId, int versionId);

        Task<IEnumerable<FamilyInfo>> GetFamyilyInfoVersionsAsync(string familyInfoId);

        Task<FamilyInfo> CreateFamilyInfoVersionAsync(FamilyInfo familyInfo);

        Task<FamilyInfo> UpdateFamilyInfoVesrionAsync(string familyInfoId, int versionId);

        Task<bool> DeleteFamilyInfoVersionsAsync(string faimilyInfoId);
    }
}
