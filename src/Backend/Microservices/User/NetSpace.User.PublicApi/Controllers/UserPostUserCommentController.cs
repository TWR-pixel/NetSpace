using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.User.Application.UserPostUserComment;
using NetSpace.User.Application.UserPostUserComment.Requests;
using NetSpace.User.UseCases;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.PublicApi.Controllers;

[ApiController]
[Route("/api/user-post-user-comments")]
public sealed class UserPostUserCommentController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserPostUserCommentResponse>>> Get([FromQuery] UserPostUserCommentFilterOptions filter,
                                                                                  [FromQuery] PaginationOptions pagination,
                                                                                  [FromQuery] SortOptions sort,
                                                                                  CancellationToken cancellationToken)
    {
        var request = new GetUserPostUserCommentRequest
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
    public async Task<ActionResult<UserPostUserCommentResponse>> Create([FromBody] UserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        var createRequest = new CreateUserPostUserCommentRequest { UserCommentRequest = request };

        var result = await Mediator.Send(createRequest, cancellationToken);

        return CreatedAtAction(nameof(Create), result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserPostUserCommentResponse>> Update([FromBody] UpdateUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserPostUserCommentResponse>> Delete([FromBody] DeleteUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}
