using NetSpace.User.Application.Common;

namespace NetSpace.Community.Application;

public abstract record CommandBase<TResponse> : RequestBase<TResponse>
{
}
