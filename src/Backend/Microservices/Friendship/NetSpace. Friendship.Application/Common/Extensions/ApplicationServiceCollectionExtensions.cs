using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Friendship.Application.User;
using NetSpace.Friendship.Application.User.Consumers;
using System.Reflection;

namespace NetSpace.Friendship.Application.Common.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(UserResponse).Assembly);
        });
        var rabbitMQOptions = config.GetSection("RabbitMQ");
        services.AddMassTransit(configure =>
        {
            configure.AddConsumers(typeof(UserDeletedConsumer).Assembly);

            configure.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(rabbitMQOptions["Host"], h =>
                {
                    h.Username(rabbitMQOptions["UserName"] ?? "guest");
                    h.Password(rabbitMQOptions["Password"] ?? "guest");
                });


                configurator.ReceiveEndpoint(e =>
                {
                    e.ConfigureConsumers(context);
                });
            });
        });

        var MapsterConfig = new TypeAdapterConfig();
        var registers = MapsterConfig.Scan(Assembly.GetAssembly(typeof(ResponseBase)) ?? Assembly.GetExecutingAssembly());
        MapsterConfig.Apply(registers);

        services.AddSingleton(config);
        services.AddSingleton<IMapper, ServiceMapper>();

        return services;
    }
}
