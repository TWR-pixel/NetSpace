using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetSpace.Identity.Api.Common;
using NetSpace.Identity.Application.User;
using NetSpace.Identity.Application.User.Commands;

namespace NetSpace.Identity.Api.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(AuthenticationSchemes = AuthConstants.AuthenticationSchemes, Policy = AuthConstants.AdminPolicy)]
public class AdminController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPost]
    [Authorize(AuthenticationSchemes = AuthConstants.AuthenticationSchemes, Policy = AuthConstants.AdminPolicy)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponse>> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(Create), result);
    }
}
