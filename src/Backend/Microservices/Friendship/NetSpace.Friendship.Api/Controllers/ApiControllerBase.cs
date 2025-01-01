using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NetSpace.Friendship.Api.Controllers;

public abstract class ApiControllerBase(IMediator mediator) : ControllerBase
{
    protected IMediator Mediator { get; private set; } = mediator;
}
