using NetSpace.User.Application.Common;

namespace NetSpace.User.Application;

public abstract record QueryBase<TResponse> : RequestBase<TResponse>
{
}
