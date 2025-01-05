using MediatR;

namespace NetSpace.User.Application.Common;

public abstract record RequestBase<TResponse> : IRequest<TResponse>
{
}
