using NetSpace.User.Application.Common;

namespace NetSpace.User.Application;

public abstract record CommandBase<TResponse> : RequestBase<TResponse>
{
}
