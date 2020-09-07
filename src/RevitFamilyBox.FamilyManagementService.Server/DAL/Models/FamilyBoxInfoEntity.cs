using System.Collections.Generic;

namespace RevitFamilyBox.FamilyManagementService.Server.DAL.Models
{
  public class FamilyBoxInfoEntity
  {
    public string Id { get; set; }

    public string Name { get; set; }

    public string Parameters { get; set; }


    public IEnumerable<FamilyBoxInfoEntity> SubFamilyBoxInfos { get; set; }

    public IEnumerable<FamilyInfoEntity> FamilyInfos { get; set; }
  }
}
