using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.User.Application.User.Queries.GetLatests;
using NetSpace.User.Application.UserPost;
using NetSpace.User.Application.UserPost.Commands.Create;
using NetSpace.User.Application.UserPost.Commands.Delete;
using NetSpace.User.Application.UserPost.Commands.PartiallyUpdate;
using NetSpace.User.Application.UserPost.Commands.Update;
using NetSpace.User.Application.UserPost.Queries.Get;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.PublicApi.Controllers;

[ApiController]
[Route("/api/user-posts")]
public class UserPostController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UserPostResponse>>> Get([FromQuery] UserPostFilterOptions filter,
                                                                       [FromQuery] PaginationOptions pagination,
                                                                       [FromQuery] SortOptions sort,
                                                                       CancellationToken cancellationToken)
    {
        var request = new GetUserPostQuery { Filter = filter, Pagination = pagination, Sort = sort };

        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("latests")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UserPostResponse>>> GetLatest([FromQuery] PaginationOptions pagination, CancellationToken cancellationToken)
    {
        var query = new GetLatestUsePostsQuery { Pagination = pagination };

        var result = await Mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserPostResponse>> Create([FromBody] CreateUserPostCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(Create), result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserPostResponse>> Update([FromBody] UpdateUserPostCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserPostResponse>> Patch([FromBody] PartiallyUpdateUserPostCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserPostResponse>> Delete([FromBody] DeleteUserPostByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

}
