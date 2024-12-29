using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.Friendship.Application.User;
using NetSpace.Friendship.Application.User.Requests;
using NetSpace.Friendship.Domain;

namespace NetSpace.Friendship.Api.Controllers;

[ApiController]
[Route("/api/friendships/")]
public sealed class FriendshipController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllByStatus([FromQuery] GetAllUserFriendsByStatusRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserEntity>>> GetAllFollowersByStatus([FromQuery] GetAllUserFollowersByStatusRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CreateFriendshipResponse>> CreateFriendship([FromBody] CreateFriendshipRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(CreateFriendship), result);
    }

    [HttpPut]
    public async Task<ActionResult<UpdateFriendshipResponse>> UpdateFriendship([FromBody] UpdateFriendshipRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}
