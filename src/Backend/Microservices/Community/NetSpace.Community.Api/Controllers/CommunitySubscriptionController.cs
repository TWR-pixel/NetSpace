﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.Application.CommunitySubscription;
using NetSpace.Community.Application.CommunitySubscription.Commands;
using NetSpace.Community.Application.CommunitySubscription.Queries;
using NetSpace.Community.UseCases.CommunitySubscription;

namespace NetSpace.Community.Api.Controllers;

[ApiController]
[Route("/api/community-subscriptions/")]
public sealed class CommunitySubscriptionController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CommunitySubscriptionResponse>>> Get([FromQuery] CommunitySubscriptionFilterOptions filter,
                                                                                    [FromQuery] PaginationOptions pagination,
                                                                                    [FromQuery] SortOptions sort,
                                                                                    CancellationToken cancellationToken)
    {
        var request = new GetCommunitySubscriptionQuery { Filter = filter, Pagination = pagination, Sort = sort };

        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CommunitySubscriptionResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var query = new GetCommunitySubscriptionWitDetailsQuery { Id = id };

        var result = await Mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunitySubscriptionResponse>> Create([FromBody] CreateCommunitySubscriptionCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(Create), result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunitySubscriptionResponse>> Update([FromBody] UpdateCommunitySubscriptionCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunitySubscriptionResponse>> Patch([FromBody] PartiallyUpdateCommunitySubuscriptionCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommunitySubscriptionResponse>> Delete([FromBody] DeleteCommunitySubscriptionCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

}
