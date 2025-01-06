using NetSpace.Community.Application.Common;

namespace NetSpace.Community.Application;

public abstract record QueryBase<TResponse> : RequestBase<TResponse>
{
}
