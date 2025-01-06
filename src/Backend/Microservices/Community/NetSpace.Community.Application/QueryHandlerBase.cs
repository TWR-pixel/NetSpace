using NetSpace.Community.Application.Common;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application;

public abstract class QueryHandlerBase<TQuery, TResponse>(IReadonlyUnitOfWork unitOfWork) : RequestHandlerBase<TQuery, TResponse>
    where TQuery : QueryBase<TResponse>
{
    protected IReadonlyUnitOfWork UnitOfWork => unitOfWork;
}
