using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.Identity.Application.User;
using NetSpace.Identity.Application.User.Commands;

namespace NetSpace.Identity.Api.Controllers;

[ApiController]
[Route("/api/identity")]
public class IdentityController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPatch("change-email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponse>> ChangeEmail([FromBody] ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPatch("change-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponse>> ChangePassword([FromBody] ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

}
