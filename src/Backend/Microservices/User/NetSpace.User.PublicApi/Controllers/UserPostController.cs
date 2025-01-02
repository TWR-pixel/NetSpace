﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.User.Application.UserPost;
using NetSpace.User.Application.UserPost.Requests;
using NetSpace.User.UseCases;
using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.PublicApi.Controllers;

[ApiController]
[Route("/api/user-posts")]
public class UserPostController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserPostResponse>>> Get([FromQuery] UserPostFilterOptions filter,
                                                                       [FromQuery] PaginationOptions pagination,
                                                                       [FromQuery] SortOptions sort,
                                                                       CancellationToken cancellationToken)
    {
        var request = new GetUserPostRequest { Filter = filter, Pagination = pagination, Sort = sort };

        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserPostResponse>> Create([FromBody] UserPostRequest request, CancellationToken cancellationToken)
    {
        var createRequest = new CreateUserPostRequest { UserPostRequest = request };

        var result = await Mediator.Send(createRequest, cancellationToken);

        return CreatedAtAction(nameof(Create), result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserPostResponse>> Update([FromBody] UpdateUserPostRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserPostResponse>> Patch([FromBody] PartiallyUpdateUserPostRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserPostResponse>> Delete([FromBody] DeleteUserPostByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

}
