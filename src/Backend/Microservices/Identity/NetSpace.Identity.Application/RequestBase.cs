using MediatR;

namespace NetSpace.Identity.Application;

public abstract record RequestBase<TResponse> : IRequest<TResponse>
{
}
