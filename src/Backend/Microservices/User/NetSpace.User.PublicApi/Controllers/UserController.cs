using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.User.Application.Requests;

namespace NetSpace.User.PublicApi.Controllers;

[ApiController]
[Route("/api/users")]
public sealed class UserController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Create(CreateUserRequest request, CancellationToken cancellationToken)
        => CreatedAtAction(nameof(Create),await Mediator.Send(request, cancellationToken));

}
