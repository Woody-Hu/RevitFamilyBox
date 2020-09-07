using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RevitFamilyBox.DAL.Models;
using RevitFamilyBox.Services;

namespace RevitFamilyBox.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class FamilyManagerController : ControllerBase
  {
    private readonly IFamilyManagerService _familyManagerService;

    private readonly ILogger<FamilyManagerController> _logger;


    public FamilyManagerController(
      ILogger<FamilyManagerController> logger, 
      IFamilyManagerService familyManagerService)
    {
      _familyManagerService = familyManagerService;

      _logger = logger;
    }

    [HttpGet("Item")]
    public async Task<IEnumerable<FamilyInfo>> GetFamilyInfosAsync()
    {
      return await _familyManagerService.GetFamilyInfoAsync(1, 1);
    }

    [HttpPost("Item")]
    public async Task<IEnumerable<FamilyInfo>> AddFamilyInfosAsync(IEnumerable<FamilyInfo> familyInfos)
    {
      return await _familyManagerService.AddFamilyInfoAsync(familyInfos);
    }
  }
}
