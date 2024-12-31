using Microsoft.AspNetCore.Mvc;

namespace NetSpace.Identity.Api.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthenticationController : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult> Register(CancellationToken cancellationToken)
    {
        return CreatedAtAction(nameof(Register), cancellationToken);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Login(CancellationToken cancellationToken)
    {
        return Ok(cancellationToken);
    }
}
