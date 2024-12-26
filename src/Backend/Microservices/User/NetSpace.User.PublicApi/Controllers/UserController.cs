using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetSpace.User.Application.User;

namespace NetSpace.User.PublicApi.Controllers;

[ApiController]
[Route("/api/users")]
public sealed class UserController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Create(UserRequest request, CancellationToken cancellationToken)
        => CreatedAtAction(nameof(Create), await Mediator.Send(request, cancellationToken));


}
