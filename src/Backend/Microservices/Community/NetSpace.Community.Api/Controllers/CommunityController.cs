using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.Community.Application.Community;
using NetSpace.Community.Application.Community.Requests.Create;
using NetSpace.Community.Application.Community.Requests.Delete;
using NetSpace.Community.Application.Community.Requests.Get;
using NetSpace.Community.Application.Community.Requests.PartiallyUpdate;
using NetSpace.Community.Application.Community.Requests.Update;
using NetSpace.Community.UseCases;
using NetSpace.Community.UseCases.Community;

namespace NetSpace.Community.Api.Controllers;

[ApiController]
[Route("/api/communities")]
public sealed class CommunityController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CommunityResponse>>> Get([FromQuery] CommunityFilterOptions filter,
                                                                        [FromQuery] PaginationOptions pagination,
                                                                        [FromQuery] SortOptions sort,
                                                                        CancellationToken cancellationToken)
    {
        var request = new GetCommunityRequest { Filter = filter, Pagination = pagination, Sort = sort };

        var result = await mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityResponse>> Create([FromBody] CreateCommunityRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(Create), result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityResponse>> Update([FromBody] UpdateCommunityRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityResponse>> Patch([FromBody] PartiallyUpdateCommunityRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunityResponse>> Delete([FromBody] DeleteCommunityRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}
