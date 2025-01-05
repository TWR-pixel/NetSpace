using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.User.Application.User;
using NetSpace.User.Application.User.Requests.Delete;
using NetSpace.User.Application.User.Requests.Get;
using NetSpace.User.Application.User.Requests.PartiallyUpdate;
using NetSpace.User.Application.User.Requests.Update;
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponse>> GetUsers([FromQuery] UserFilterOptions filter,
                                                           [FromQuery] SortOptions sort,
                                                           [FromQuery] PaginationOptions pagination,
                                                           CancellationToken cancellationToken)
    {
        var request = new GetUsersRequest { Filter = filter, Pagination = pagination, Sort = sort };

        return Ok(await Mediator.Send(request, cancellationToken));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponse>> Update(UpdateUserRequest request, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(request, cancellationToken));

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponse>> Patch(PartiallyUpdateUserRequest request, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(request, cancellationToken));

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponse>> DeleteById(DeleteUserByIdRequest request, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(request, cancellationToken));
}
