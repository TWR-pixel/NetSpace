using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Friendship.Application.User;
using NetSpace.Friendship.Application.User.Consumers;
using System.Reflection;

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
                    e.ConfigureConsumers(context);
                });
            });
        });

        var config = new TypeAdapterConfig();
        var registers = config.Scan(Assembly.GetAssembly(typeof(ResponseBase)) ?? Assembly.GetExecutingAssembly());
        config.Apply(registers);

        services.AddSingleton(config);
        services.AddSingleton<IMapper, ServiceMapper>();

        return services;
    }
}
