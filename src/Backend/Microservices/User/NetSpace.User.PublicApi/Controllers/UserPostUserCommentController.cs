using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NetSpace.User.PublicApi.Controllers;

[ApiController]
[Route("/api/user-post-user-comments")]
public sealed class UserPostUserCommentController(IMediator mediator) : ApiControllerBase(mediator)
{
}
