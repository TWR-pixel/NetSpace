using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetSpace.User.Application.User.Requests;

namespace NetSpace.User.PublicApi.Controllers;

[ApiController]
[Route("/api/auth")]
public sealed class AuthenticationController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine("LOG REGISTER IJO");
        //var response = await Mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(Register), "sijf");
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserRequest request, CancellationToken cancellationToken)
    {


        return Ok();
    }
}
