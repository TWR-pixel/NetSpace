using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.User.Application.Requests;

namespace NetSpace.User.PublicApi.Controllers;

public sealed class AuthenticationController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPost]
    public async Task<ActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

}
