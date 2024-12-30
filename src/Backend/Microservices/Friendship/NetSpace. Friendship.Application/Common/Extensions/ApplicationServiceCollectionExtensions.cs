using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Friendship.Application.User;
using NetSpace.Friendship.Application.User.Consumers;

namespace NetSpace.Friendship.Application.Common.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(UserResponse).Assembly);
        });

        services.AddMassTransit(configure =>
        {
            configure.AddConsumers(typeof(UserDeletedConsumer).Assembly);

            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.ReceiveEndpoint(e =>
                {
                    e.ConfigureConsumer<UserDeletedConsumer>(context);
                });
            });
        });

        return services;
    }
}
