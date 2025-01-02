using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Common.Messages.Email;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.Common.Cache;
using NetSpace.User.Infrastructure.Common.Cache;
using NetSpace.User.Infrastructure.User;
using NetSpace.User.Infrastructure.UserPost;
using NetSpace.User.Infrastructure.UserPostUserComment;
using NetSpace.User.UseCases.User;
using NetSpace.User.UseCases.UserPost;
using NetSpace.User.UseCases.UserPostUserComment;

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
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserPostRepository, UserPostRepository>();
        services.AddScoped<IUserPostUserCommentRepository, UserPostUserCommentRepository>();
        services.AddScoped<IUserDistributedCacheStorage, UserDistributedCacheStorage>();

        return services;
    }
}
