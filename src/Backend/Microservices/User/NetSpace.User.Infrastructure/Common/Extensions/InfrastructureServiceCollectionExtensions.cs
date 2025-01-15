using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Common.Injector.Extensions;
using NetSpace.Common.Messages.Email;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.User.Consumers;

namespace NetSpace.User.Infrastructure.Common.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddMassTransit(configure =>
        {
            configure.AddConsumers(typeof(UserCreatedConsumer).Assembly);

            var rabbitMQOptions = config.GetSection("RabbitMQ");
            configure.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(rabbitMQOptions["Host"], h =>
                {
                    h.Username(rabbitMQOptions["UserName"] ?? "guest");
                    h.Password(rabbitMQOptions["Password"] ?? "guest");
                });

                configurator.ReceiveEndpoint(e =>
                {
                    configurator.ConfigureEndpoints(context);
                });

                configurator.Publish<UserCreatedMessage>();
                configurator.Publish<SendEmailMessage>();
                configurator.Publish<UserDeletedMessage>();
                configurator.Publish<UserUpdatedMessage>();
            });
        });

        services.AddStackExchangeRedisCache(configure =>
        {
            configure.InstanceName = config["Redis:InstanceName"];
            configure.Configuration = config.GetConnectionString("Redis");
        });

        services.AddDbContext<NetSpaceDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("PostgreSql"));
        });

        services.RegisterInjectServicesFromAssembly(typeof(NetSpaceDbContext).Assembly);

        return services;
    }
}
