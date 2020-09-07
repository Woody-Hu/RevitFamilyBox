using RevitFamilyBox.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevitFamilyBox.Services
{
  public interface IFamilyManagerService
  {
    Task<IEnumerable<FamilyInfo>> AddFamilyInfoAsync(IEnumerable<FamilyInfo> familyInfos);

    Task<IEnumerable<FamilyInfo>> GetFamilyInfoAsync(int userId, int versionId, string xPath = null);

    Task<bool> RemoveFamilyInfoAsync(IEnumerable<int> familyIds, int versionId);

    Task<bool> UpdateFamilyInfoAsync(IEnumerable<FamilyInfo> familyInfos);
  }
}
