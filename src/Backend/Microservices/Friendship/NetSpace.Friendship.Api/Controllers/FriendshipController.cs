﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.Friendship.Application.User;
using NetSpace.Friendship.Application.User.Requests;
using NetSpace.Friendship.Domain.User;

namespace NetSpace.Friendship.Api.Controllers;

[ApiController]
[Route("/api/friendships/")]
public sealed class FriendshipController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllByStatus([FromQuery] GetAllUserFriendsByStatusRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("followers")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<UserEntity>>> GetAllFollowersByStatus([FromQuery] GetAllUserFollowersByStatusRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("possible-friends")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetPossibleFriends([FromQuery] GetPossibleFriendsRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("followers-count")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetFollowersCount([FromQuery] GetAllUserFollowersCountRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("friends-count")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetFollowersCount([FromQuery] GetAllUserFriendsCountRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CreateFriendshipResponse>> CreateFriendship([FromBody] CreateFriendshipRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(CreateFriendship), result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateFriendshipResponse>> UpdateFriendship([FromBody] UpdateFriendshipRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }


}
