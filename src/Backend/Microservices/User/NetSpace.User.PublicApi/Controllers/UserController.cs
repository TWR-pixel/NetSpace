using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.User.Application.User;
using NetSpace.User.Application.User.Commands.Delete;
using NetSpace.User.Application.User.Commands.PartiallyUpdate;
using NetSpace.User.Application.User.Commands.Update;
using NetSpace.User.Application.User.Queries.Get;
using NetSpace.User.UseCases.Common;
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
        var request = new GetUsersQuery { Filter = filter, Pagination = pagination, Sort = sort };

        return Ok(await Mediator.Send(request, cancellationToken));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponse>> Update(UpdateUserCommand request, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(request, cancellationToken));

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponse>> Patch(PartiallyUpdateUserCommand request, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(request, cancellationToken));

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponse>> DeleteById(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(request, cancellationToken));
}
