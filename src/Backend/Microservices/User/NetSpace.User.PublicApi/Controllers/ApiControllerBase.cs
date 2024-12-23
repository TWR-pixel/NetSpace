using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NetSpace.User.PublicApi.Controllers;

public abstract class ApiControllerBase(IMediator mediator) : ControllerBase
{
    protected IMediator Mediator { get; private set; } = mediator;
}
