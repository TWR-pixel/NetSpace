using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NetSpace.Identity.Api.Controllers;

public abstract class ApiControllerBase(IMediator mediator) : ControllerBase
{
    protected IMediator Mediator { get; } = mediator;
}
