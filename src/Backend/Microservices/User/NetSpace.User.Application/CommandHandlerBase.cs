using NetSpace.User.Application.Common;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application;

public abstract class CommandHandlerBase<TCommand, TResponse>(IUnitOfWork unitOfWork) : RequestHandlerBase<TCommand, TResponse>
    where TCommand : CommandBase<TResponse>
{
    protected IUnitOfWork UnitOfWork => unitOfWork;
}
