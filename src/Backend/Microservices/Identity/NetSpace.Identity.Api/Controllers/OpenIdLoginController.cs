using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Api.Controllers;

[ApiController]
[Route("/api/openId-login/")]
public class OpenIdLoginController() : ControllerBase
{

    //[HttpGet("google-callback")]
    ////[Authorize]
    //public async Task<ActionResult> GoogleCallback(CancellationToken cancellationToken)
    //{
    //    var user = this.HttpContext.User;

    //    if (user?.Identity != null && user.Identity.IsAuthenticated)
    //    {
    //        return this.Ok(new UserModel
    //        {
    //            IsAuthenticated = true,
    //            Name = user.FindFirst("name")?.Value,
    //            EmailAddress = user.FindFirst(c => c.Type == ClaimTypes.Email)?.Value,
    //            Phone = user.FindFirst(c => c.Type == "phone")?.Value
    //        });
    //    }

    //    return this.Ok(new UserModel
    //    {
    //        IsAuthenticated = false,
    //    });
    //}

    [Authorize]
    [HttpGet("login")]
    public async Task<ActionResult> AuthorizeAsync()
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

    [HttpGet("logout")]
    public async Task UnAuthorize()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            return;
        }
    }



}

public class UserModel
{
    public bool IsAuthenticated { get; set; }
    public string? Name { get; set; }
    public string? EmailAddress { get; set; }
    public string? Phone { get; set; }
}