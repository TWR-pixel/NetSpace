using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;
using NetSpace.Identity.Application.User.Requests;

namespace NetSpace.Identity.Api.Controllers;

[ApiController]
[Route("/api/jwt-auth")]
public sealed class JwtBearerAuthController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AccessTokenResponse>> RegisterByJwt([FromBody] JwtUserRegistrationRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(RegisterByJwt), result);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AccessTokenResponse>> LoginByJwt([FromBody] JwtUserLoginRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}
