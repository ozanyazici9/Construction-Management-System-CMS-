using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/{v:version}/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
public class AuthenticationController : ControllerBase
{
    [HttpPost("register-company")]
    public IActionResult RegisterCompany()
    {
        return Ok();
    }

    [HttpPost("accept-invite")]
    public IActionResult AcceptInviteAndRegister()
    {
        return Ok() ;
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
        return Ok();
    }
}
