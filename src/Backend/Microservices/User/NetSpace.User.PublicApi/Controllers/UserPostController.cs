using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NetSpace.User.PublicApi.Controllers;

[ApiController]
[Route("/api/user-posts")]
public class UserPostController(IMediator mediator) : ApiControllerBase(mediator)
{
}
