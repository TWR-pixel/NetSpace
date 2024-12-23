using Microsoft.Extensions.DependencyInjection;

namespace NetSpace.User.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(UserRequest<>).Assembly);
        });

        return services;
    }
}
