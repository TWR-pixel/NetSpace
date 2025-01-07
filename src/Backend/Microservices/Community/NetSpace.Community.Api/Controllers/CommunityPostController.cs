using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.Application.CommunityPost.Commands;
using NetSpace.Community.Application.CommunityPost.Queries;
using NetSpace.Community.UseCases.CommunityPost;
using NetSpace.Community.Application.CommunityPost;

namespace NetSpace.Community.Api.Controllers;

[ApiController]
[Route("/api/community-posts/")]
public sealed class CommunityPostController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CommunityPostResponse>>> Get([FromQuery] CommunityPostFilterOptions filter,
                                                                            [FromQuery] PaginationOptions pagination,
                                                                            [FromQuery] SortOptions sort,
                                                                            CancellationToken cancellationToken)
    {
        var request = new GetCommunityPostQuery { Filter = filter, Pagination = pagination, Sort = sort };

        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CommunityPostResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var query = new GetByIdCommunityPostQuery { Id = id };

        var result = await Mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityPostResponse>> Create([FromBody] CreateCommunityPostCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(Create), result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityPostResponse>> Update([FromBody] UpdateCommunityPostCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityPostResponse>> Patch([FromBody] PartiallyUpdateCommunityPostCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityPostResponse>> Delete([FromBody] DeleteCommunityPostCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}
