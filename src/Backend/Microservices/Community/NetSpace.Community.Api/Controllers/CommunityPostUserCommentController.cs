using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.Community.Application.CommunityPostUserComment;
using NetSpace.Community.UseCases.CommunityPostUserComment;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.Application.CommunityPostUserComment.Queries;
using NetSpace.Community.Application.CommunityPostUserComment.Commands;

namespace NetSpace.Community.Api.Controllers;

[ApiController]
[Route("/api/community-post-user-comments/")]
public sealed class CommunityPostUserCommentController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CommunityPostUserCommentResponse>>> Get([FromQuery] CommunityPostUsercommentFilterOptions filter,
                                                                                       [FromQuery] PaginationOptions pagination,
                                                                                       [FromQuery] SortOptions sort,
                                                                                       CancellationToken cancellationToken)
    {
        var request = new GetCommunityPostUserCommentQuery { Filter = filter, Pagination = pagination, Sort = sort };

        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CommunityPostUserCommentResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var query = new GetCommunityPostUserCommentByIdQuery { Id = id };

        var result = await Mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityPostUserCommentResponse>> Create([FromBody] CreateCommunityPostUserCommentCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(Create), result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityPostUserCommentResponse>> Update([FromBody] UpdateCommunityPostUserCommentCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityPostUserCommentResponse>> Patch([FromBody] PartiallyUpdateCommunityPostUserCommentCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityPostUserCommentResponse>> Delete([FromBody] DeleteCommunityPostUserCommentCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}
