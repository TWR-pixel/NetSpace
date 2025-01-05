using MediatR;

namespace NetSpace.Community.Application.Common;

public abstract record RequestBase<TResponse> : IRequest<TResponse>
{
}
