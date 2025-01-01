
namespace NetSpace.Friendship.Api.Middlewares;

public sealed class HandleExceptionMiddleware : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        throw new NotImplementedException();
    }
}
