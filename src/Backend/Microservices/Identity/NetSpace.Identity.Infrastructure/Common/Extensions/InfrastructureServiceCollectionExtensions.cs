using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Identity.Application.User.Consumers;
using NetSpace.Identity.Domain.User;
using NetSpace.Identity.Infrastructure.User;
using NetSpace.Identity.UseCases.User;

namespace NetSpace.Identity.Infrastructure.Common.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<NetSpaceDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddIdentityCore<UserEntity>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<NetSpaceDbContext>();

        services.AddScoped<IUserRepository, UserRepository>();


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
