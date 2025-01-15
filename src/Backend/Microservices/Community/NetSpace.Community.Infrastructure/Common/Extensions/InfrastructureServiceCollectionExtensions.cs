using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Common.Injector.Extensions;
using NetSpace.Community.Application.Community.Commands;

namespace NetSpace.Community.Infrastructure.Common.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString, string redisInstanceName, IConfiguration config)
    {
        services.AddDbContext<NetSpaceDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        var rabbitMQOptions = config.GetSection("RabbitMQ");
        services.AddMassTransit(config =>
        {
            config.AddConsumers(typeof(UpdateCommunityCommand).Assembly);

            config.UsingRabbitMq((context, configurator) =>
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

        services.AddStackExchangeRedisCache(configure =>
        {
            configure.InstanceName = redisInstanceName;
            configure.Configuration = config.GetConnectionString("Redis");
        });

        services.RegisterInjectServicesFromAssembly(typeof(NetSpaceDbContext).Assembly);

        return services;
    }
}
