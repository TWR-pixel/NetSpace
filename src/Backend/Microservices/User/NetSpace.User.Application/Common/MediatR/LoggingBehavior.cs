using MediatR;
using Microsoft.Extensions.Logging;
using NetSpace.Common.Application;
using NetSpace.User.Application.User.Exceptions;

namespace NetSpace.User.Application.Common.MediatR;

public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : RequestBase<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            var response = await next();

            return response;
        }
        catch (UserNotFoundException ex)
        {
            logger.LogError(ex, "");

            throw;
        }

    }
}
