using MediatR;

namespace NetSpace.UserPosts.Application;

public abstract record RequestBase<TResponse> : IRequest<TResponse>
{
}
