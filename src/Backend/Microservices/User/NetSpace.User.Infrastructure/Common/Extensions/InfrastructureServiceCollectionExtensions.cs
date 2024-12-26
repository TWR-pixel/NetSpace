using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Common.Messages.User;
using NetSpace.User.Domain;
using NetSpace.User.UseCases;

namespace NetSpace.User.Infrastructure.Common.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMassTransit(configure =>
        {
            configure.UsingRabbitMq((busContext, factoryConfigurator) =>
            {
                factoryConfigurator.Publish<UserCreatedMessage>(conf =>
                {

                });
            });
        });

        //services.AddScoped<IPublisher, RabbitMQPublisher>();

        services
            .AddIdentity<UserEntity, IdentityRole>()
            .AddEntityFrameworkStores<NetSpaceDbContext>()
            .AddDefaultTokenProviders();

        services.AddDbContext<NetSpaceDbContext>(options =>
        {
        });

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
