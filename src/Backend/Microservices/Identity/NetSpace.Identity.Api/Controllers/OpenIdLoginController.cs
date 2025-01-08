using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using NetSpace.Identity.Application.User.Commands;
using NetSpace.Identity.Application.User;
using NetSpace.Identity.Application.User.Exceptions;

namespace NetSpace.Identity.Api.Controllers;

[ApiController]
[Route("/api/openId-login/")]
public class OpenIdLoginController(IMediator mediator) : ApiControllerBase(mediator)
{
    [Authorize]
    [HttpGet("google-login")]
    public async Task<ActionResult<UserResponse>> GoogleLoginAsync(CancellationToken cancellationToken)
    {
        try
        {
            var request = new GoogleExternalLoginCommand { User = User };

            var result = await Mediator.Send(request, cancellationToken);

            return Ok(result);
        }
        catch (UserAlreadyExistsException alreadyExists)
        {
            return Conflict(alreadyExists.Message);
        }
    }

    [HttpGet("google-logout")]
    public async Task<ActionResult> UnAuthorize()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete("oidc");
            return Ok();
        }

        return Ok();
    }



}