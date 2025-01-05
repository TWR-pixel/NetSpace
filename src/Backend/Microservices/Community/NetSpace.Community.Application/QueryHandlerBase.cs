using NetSpace.User.Application.Common;
using NetSpace.User.UseCases.Common;

namespace NetSpace.Community.Application;

public abstract class QueryHandlerBase<TQuery, TResponse>(IReadonlyUnitOfWork unitOfWork) : RequestHandlerBase<TQuery, TResponse>
    where TQuery : QueryBase<TResponse>
{
    protected IReadonlyUnitOfWork UnitOfWork => unitOfWork;
}
