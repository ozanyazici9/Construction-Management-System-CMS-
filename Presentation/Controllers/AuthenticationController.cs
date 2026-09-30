using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers;

[ApiController]
[Route("api/{v:apiversion}/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
public class AuthenticationController : ControllerBase
{
    private readonly IServiceManager _service;

    public AuthenticationController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost("register-company")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RegisterCompany([FromBody] CompanyForRegistrationDto companyForRegistrationDto)
    {
        var result = await _service.AuthenticationService.RegisterCompanyAsync(companyForRegistrationDto);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        return StatusCode(201);
    }

    [HttpPost("accept-invite")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> AcceptInviteAndRegister(
        [FromBody] UserForRegistrationDto userForRegistrationDto
    )
    {
        var result = await _service.AuthenticationService.RegisterUser(userForRegistrationDto);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return StatusCode(201);
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
        return Ok();
    }
}
