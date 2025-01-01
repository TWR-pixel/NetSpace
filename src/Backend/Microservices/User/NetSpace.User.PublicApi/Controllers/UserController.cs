using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetSpace.User.Application.User;
using NetSpace.User.Application.User.Requests;
using NetSpace.User.UseCases;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.PublicApi.Controllers;

[ApiController]
[Route("/api/users")]
public sealed class UserController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponse>> GetUsers([FromQuery] UserFilterOptions filter, SortOptions sorting, PaginationOptions pagination, CancellationToken cancellationToken)
    {
        var request = new GetUsersRequest { FilterOptions = filter };

        return Ok(await Mediator.Send(request, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserResponse>> Create(UserRequest request, CancellationToken cancellationToken)
        => CreatedAtAction(nameof(Create), await Mediator.Send(request, cancellationToken));

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponse>> Update(UpdateUserRequest request, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(request, cancellationToken));

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponse>> DeleteById(DeleteUserByIdRequest request, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(request, cancellationToken));
}
