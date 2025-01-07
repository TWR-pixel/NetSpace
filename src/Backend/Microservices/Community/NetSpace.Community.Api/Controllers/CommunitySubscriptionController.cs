using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NetSpace.Community.Api.Controllers;

[ApiController]
[Route("/api/community-subscriptions/")]
public sealed class CommunitySubscriptionController(IMediator mediator) : ApiControllerBase(mediator)
{


}
