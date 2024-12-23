using MediatR;

namespace NetSpace.Common.Application;

public abstract record RequestBase<TResponse> : IRequest<TResponse>
{
}
