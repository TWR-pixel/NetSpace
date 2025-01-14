using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<NetSpaceDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("PostgreSql"));
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

        services.AddScoped<IEmailSender<UserEntity>, EmailSenderOfTUser>();
        services.AddScoped<IEmailSender, EmailSender>();

        services.AddScoped<IUserRepository, UserRepository>();

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
                    configurator.ConfigureEndpoints(context);
                });

                configurator.Publish<UserCreatedMessage>();
                configurator.Publish<UserUpdatedMessage>();
            });
        });

        return services;
    }
}
