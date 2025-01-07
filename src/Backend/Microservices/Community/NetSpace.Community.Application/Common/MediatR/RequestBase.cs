using MediatR;

namespace NetSpace.Community.Application.Common.MediatR;

public abstract record RequestBase<TResponse> : IRequest<TResponse>
{
}
