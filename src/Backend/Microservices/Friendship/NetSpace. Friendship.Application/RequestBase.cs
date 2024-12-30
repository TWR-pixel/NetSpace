using MediatR;

namespace NetSpace.Friendship.Application;

public abstract record RequestBase<TResponse> : IRequest<TResponse>
{
}
