using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.User.Application.UserPostUserComment;
using NetSpace.User.Application.UserPostUserComment.Commands.Create;
using NetSpace.User.Application.UserPostUserComment.Commands.Delete;
using NetSpace.User.Application.UserPostUserComment.Commands.PartiallyUpdate;
using NetSpace.User.Application.UserPostUserComment.Commands.Update;
using NetSpace.User.Application.UserPostUserComment.Queries.Get;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.PublicApi.Controllers;

[ApiController]
[Route("/api/user-post-user-comments")]
public sealed class UserPostUserCommentController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UserPostUserCommentResponse>>> Get([FromQuery] UserPostUserCommentFilterOptions filter,
                                                                                  [FromQuery] PaginationOptions pagination,
                                                                                  [FromQuery] SortOptions sort,
                                                                                  CancellationToken cancellationToken)
    {
        var request = new GetUserPostUserCommentQuery
        {
            Filter = filter,
            Pagination = pagination,
            Sort = sort
        };

        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserPostUserCommentResponse>> Create([FromBody] CreateUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(Create), result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserPostUserCommentResponse>> Update([FromBody] UpdateUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserPostUserCommentResponse>> Patch([FromBody] PartiallyUpdateUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserPostUserCommentResponse>> Delete([FromBody] DeleteUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}
