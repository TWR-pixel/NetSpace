using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Friendship.Application.User.Consumers;

namespace NetSpace.Friendship.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplications(this IServiceCollection services)
    {
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
