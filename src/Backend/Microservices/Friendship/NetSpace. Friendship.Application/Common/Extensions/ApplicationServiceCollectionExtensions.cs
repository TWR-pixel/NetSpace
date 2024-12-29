using Microsoft.Extensions.DependencyInjection;
using NetSpace.Friendship.Application.User;

namespace NetSpace.Friendship.Application.Common.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(UserResponse).Assembly);
        });

        return services;
    }
}
