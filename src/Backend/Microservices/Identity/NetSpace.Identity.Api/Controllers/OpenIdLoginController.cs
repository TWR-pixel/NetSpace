using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize]
    [HttpGet("login")]
    public ActionResult Authorize()
    {
        return this.RedirectPermanent("/me");
    }

    [HttpGet("logout")]
    public async Task UnAuthorize()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            return;
        }

        // Handle the case when the user is not authenticated
        HttpContext.Response.Redirect("/logout-google");
    }

    [HttpGet("/logout-google")]
    public string LogoutGoogle()
    {
        return "Logout google";
    }

    [HttpGet("me")]
    public ActionResult<UserModel> GetUserProfile()
    {
        var user = this.HttpContext.User;

        if (user?.Identity != null && user.Identity.IsAuthenticated)
        {
            return this.Ok(new UserModel
            {
                IsAuthenticated = true,
                Name = user.FindFirst("name")?.Value,
                EmailAddress = user.FindFirst(c => c.Type == ClaimTypes.Email)?.Value,
                Phone = user.FindFirst(c => c.Type == "phone")?.Value
            });
        }

        return this.Ok(new UserModel
        {
            IsAuthenticated = false,
        });
    }

}

public class UserModel
{
    public bool IsAuthenticated { get; set; }
    public string? Name { get; set; }
    public string? EmailAddress { get; set; }
    public string? Phone { get; set; }
}