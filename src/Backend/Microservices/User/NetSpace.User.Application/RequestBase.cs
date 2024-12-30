using MediatR;

namespace NetSpace.User.Application;

public abstract record RequestBase<TResponse> : IRequest<TResponse>
{
}
