using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;

namespace NetSpace.Identity.Api.Common;

public static class HealthChecksServiceCollectionExtensions
{
    public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddSingleton(sp =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = config["RabbitMQ:Host"] ?? "localhost",
                    Port = int.Parse(config["RabbitMQ:Port"] ?? "5673"),
                    UserName = config["RabbitMQ:UserName"] ?? "guest",
                    Password = config["RabbitMQ:Password"] ?? "guest"
                };

                return factory.CreateConnectionAsync().GetAwaiter().GetResult();
            })
            .AddHealthChecks()
            .AddRabbitMQ();


        services
            .AddHealthChecks()
            .AddNpgSql(config.GetConnectionString("PostgreSql") ?? "Host=localhost");

        services
            .AddHealthChecks()
            .AddRedis(config.GetConnectionString("Redis") ?? "localhost");

        return services;
    }
}
