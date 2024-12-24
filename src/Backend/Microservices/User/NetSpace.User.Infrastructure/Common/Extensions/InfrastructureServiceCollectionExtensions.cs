using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.Common.MessageBroker;
using NetSpace.User.Domain;
using NetSpace.User.Infrastructure.Common;

namespace NetSpace.User.Infrastructure.Common.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMassTransit(configure =>
        {
            configure.UsingRabbitMq((busContext, factoryConfigurator) =>
            {
                factoryConfigurator.Publish<UserCreatedMessage>();
            });
        });

        services.AddScoped<IPublisher, RabbitMQPublisher>();

        services
            .AddIdentityCore<UserEntity>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<NetSpaceDbContext>();


        return services;
    }
}
