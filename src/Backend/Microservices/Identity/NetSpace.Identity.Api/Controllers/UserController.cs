using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetSpace.Identity.Application.User;
using NetSpace.Identity.Application.User.Commands.Create;

namespace NetSpace.Identity.Api.Controllers;

public sealed class UserController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<UserResponse>> Create(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(Create), result);
    }
}
