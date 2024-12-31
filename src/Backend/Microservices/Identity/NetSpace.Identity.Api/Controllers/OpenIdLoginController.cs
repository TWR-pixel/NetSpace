using Microsoft.AspNetCore.Mvc;

namespace NetSpace.Identity.Api.Controllers;

[ApiController]
[Route("/api/openId-login/")]
public class OpenIdLoginController : ControllerBase
{
    [HttpGet("google-login")]
    public async Task<ActionResult> GoogleLogin(CancellationToken cancellationToken)
    {
        return Ok();
    }


}
