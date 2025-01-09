using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetSpace.Identity.Api.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Policy = "RequiredAdminClaim")]
public class AdminController(IMediator mediator) : ApiControllerBase(mediator)
{
}
