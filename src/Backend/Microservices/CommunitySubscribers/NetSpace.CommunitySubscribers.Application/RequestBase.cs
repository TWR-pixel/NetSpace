using MediatR;

namespace NetSpace.CommunitySubscribers.Application;

public abstract record RequestBase<TResponse> : IRequest<TResponse>
{
}
