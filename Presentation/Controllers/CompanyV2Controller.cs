using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers;

[ApiController]
[Route("api/{v:apiversion}/companies")]
[ApiExplorerSettings(GroupName = "v2")]
public class CompanyV2Controller : ControllerBase
{
    private readonly IServiceManager _manager;

    public CompanyV2Controller(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompanies()
    {
        var companies = await _manager.Company.GetAllCompaniesAsync(trackChanges: false);

        return Ok(companies);
    }
}
