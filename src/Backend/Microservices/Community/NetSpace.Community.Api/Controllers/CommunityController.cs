﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.Community.Application.Community.Requests;
using NetSpace.Community.Domain.Community;
using NetSpace.Community.UseCases.Community;

namespace NetSpace.Community.Api.Controllers;

[ApiController]
[Route("/api/communities")]
public sealed class CommunityController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommunityEntity>?>> GetByFilter([FromQuery] CommunityFilterOptions filter, CancellationToken cancellationToken)
    {
        var request = new GetCommunityRequest() { Filter = filter };
        var result = await mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}
