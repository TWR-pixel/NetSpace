using NetSpace.Community.Application.Common.MediatR;

namespace NetSpace.Community.Application;

public abstract record QueryBase<TResponse> : RequestBase<TResponse>
{
}
