using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RevitFamilyBox.FamilyManagementService.Server.Domain;

namespace RevitFamilyBox.FamilyManagementService.Server.Controllers
{
    [ApiController]
  [Route("[controller]")]
  public class FamilyManagerController : ControllerBase
  {
    private readonly IFamilyInfoRepository _familyManagerService;

    private readonly ILogger<FamilyManagerController> _logger;


    public FamilyManagerController(
      ILogger<FamilyManagerController> logger,
      IFamilyInfoRepository familyManagerService)
    {
      _familyManagerService = familyManagerService;

      _logger = logger;
    }

    [HttpGet("Item")]
    public async Task<IEnumerable<FamilyInfo>> GetFamilyInfosAsync()
    {
      return await _familyManagerService.FindFamilyInfosAsync(1, string.Empty);
    }

    [HttpPost("Item")]
    public async Task<IEnumerable<FamilyInfo>> AddFamilyInfosAsync(IEnumerable<FamilyInfo> familyInfos)
    {
      return await _familyManagerService.AddFamilyInfosAsync(familyInfos);
    }
  }
}
