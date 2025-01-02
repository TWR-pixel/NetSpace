using MediatR;
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
    public async Task<ActionResult<UserResponse>> GetUsers([FromQuery] UserFilterOptions filter,
                                                           [FromQuery] SortOptions sort,
                                                           [FromQuery] PaginationOptions pagination,
                                                           CancellationToken cancellationToken)
    {
        var request = new GetUsersRequest { Filter = filter, Pagination = pagination, Sort = sort };

        return Ok(await Mediator.Send(request, cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserResponse>> Create(UserRequest request, CancellationToken cancellationToken)
        => CreatedAtAction(nameof(Create), await Mediator.Send(request, cancellationToken));

    [HttpPut]
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
