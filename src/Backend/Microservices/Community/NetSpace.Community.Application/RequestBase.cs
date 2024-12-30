using MediatR;

namespace NetSpace.Community.Application;

public abstract record RequestBase<TResponse> : IRequest<TResponse>
{
}
