using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.Community.Application.Community;
using NetSpace.Community.Application.Community.Commands;
using NetSpace.Community.Application.Community.Queries;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.UseCases.Community;

namespace NetSpace.Community.Api.Controllers;

[ApiController]
[Route("/api/communities")]
public sealed class CommunityController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CommunityResponse>>> Get([FromQuery] CommunityFilterOptions filter,
                                                                        [FromQuery] PaginationOptions pagination,
                                                                        [FromQuery] SortOptions sort,
                                                                        CancellationToken cancellationToken)
    {
        var request = new GetCommunityQuery { Filter = filter, Pagination = pagination, Sort = sort };

        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CommunityResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var query = new GetCommunityByIdQuery { Id = id };

        var result = await Mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityResponse>> Create([FromBody] CreateCommunityCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(Create), result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityResponse>> Update([FromBody] UpdateCommunityCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityResponse>> Patch([FromBody] PartiallyUpdateCommunityCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityResponse>> Delete([FromBody] DeleteCommunityCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}
