using NetSpace.Community.Application.Common.MediatR;

namespace NetSpace.Community.Application;

public abstract record CommandBase<TResponse> : RequestBase<TResponse>
{
}
