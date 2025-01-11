using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Common.Messages.User;
using NetSpace.Identity.Application.User.Consumers;
using NetSpace.Identity.Domain.User;
using NetSpace.Identity.Infrastructure.Common.Email;
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
            options.SignIn.RequireConfirmedEmail = true;
            options.User.AllowedUserNameCharacters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_@";
        })
            .AddRoles<IdentityRole>()
            .AddDefaultTokenProviders()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<NetSpaceDbContext>();

        services.AddScoped<IEmailSender<UserEntity>, UserEmailSender>();
        services.AddScoped<IEmailSender, EmailSender>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddMassTransit(configure =>
        {
            configure.AddConsumers(typeof(UserDeletedConsumer).Assembly);

            configure.UsingRabbitMq((context, configurator) =>
            {
                configurator.ReceiveEndpoint(e =>
                {
                    configurator.ConfigureEndpoints(context);
                });

                configurator.Publish<UserCreatedMessage>();
                configurator.Publish<UserUpdatedMessage>();
            });
        });

        return services;
    }
}
