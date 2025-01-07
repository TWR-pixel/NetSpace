using NetSpace.Community.Application.Common.MediatR;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application;

public abstract class CommandHandlerBase<TCommand, TResponse>(IUnitOfWork unitOfWork) : RequestHandlerBase<TCommand, TResponse>
    where TCommand : CommandBase<TResponse>
{
    protected IUnitOfWork UnitOfWork => unitOfWork;
}
