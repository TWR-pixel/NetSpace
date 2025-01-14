using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Common.Messages.Email;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.User;
using NetSpace.User.Application.User.Consumers;
using NetSpace.User.Application.UserPost;
using NetSpace.User.Application.UserPostUserComment;
using NetSpace.User.Infrastructure.User;
using NetSpace.User.Infrastructure.UserPost;
using NetSpace.User.Infrastructure.UserPostUserComment;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.User;
using NetSpace.User.UseCases.UserPost;
using NetSpace.User.UseCases.UserPostUserComment;

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

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserPostRepository, UserPostRepository>();
        services.AddScoped<IUserPostUserCommentRepository, UserPostUserCommentRepository>();

        services.AddScoped<IUserDistributedCacheStorage, UserDistributedCacheStorage>();
        services.AddScoped<IUserPostDistributedCacheStorage, UserPostDistributedCacheStorage>();
        services.AddScoped<IUserPostUsercommentDistrubutedCacheStorage, UserPostUserCommentDistrubutedCacheStorage>();

        services.AddScoped<IUserReadonlyRepository, UserRepository>();
        services.AddScoped<IUserPostReadonlyRepository, UserPostRepository>();
        services.AddScoped<IUserPostUserCommentReadonlyRepository, UserPostUserCommentRepository>();

        services.AddScoped<IReadonlyUnitOfWork, ReadonlyUnitOfWork>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
