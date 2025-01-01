using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Common.Messages;
using NetSpace.Common.Messages.User;
using NetSpace.User.Infrastructure.User;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Infrastructure.Common.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString, IConfiguration config)
    {
        services.AddMassTransit(configure =>
        {
            configure.UsingRabbitMq((busContext, factoryConfigurator) =>
            {
                factoryConfigurator.Publish<UserCreatedMessage>();
                factoryConfigurator.Publish<SendEmailMessage>();
                factoryConfigurator.Publish<UserDeletedMessage>();
                factoryConfigurator.Publish<UserUpdatedMessage>();
            });
        });

        //services.AddScoped<IPublisher, RabbitMQPublisher>();

        services.AddStackExchangeRedisCache(configure =>
        {
            configure.InstanceName = config["Redis:InstanceName"];
            configure.Configuration = config.GetConnectionString("Redis");
        });

        services.AddDbContext<NetSpaceDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            if (bool.Parse(config["UseSeeding"]) == true)
            {
                options.UseSeeding((seed, _) =>
                {
                    NetSpaceDbContextSeeding.Seed(seed);
                });
            }
        });

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
