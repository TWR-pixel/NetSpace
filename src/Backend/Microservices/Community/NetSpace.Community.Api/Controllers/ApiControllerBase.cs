using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NetSpace.Community.Api.Controllers;

public abstract class ApiControllerBase(IMediator mediator) : ControllerBase
{
    protected IMediator Mediator => mediator;
}
