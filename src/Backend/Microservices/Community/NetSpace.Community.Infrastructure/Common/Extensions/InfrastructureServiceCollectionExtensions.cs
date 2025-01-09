﻿using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Community.Application.Community.Caching;
using NetSpace.Community.Application.Community.Commands;
using NetSpace.Community.Application.CommunityPost.Caching;
using NetSpace.Community.Application.CommunityPostUserComment.Caching;
using NetSpace.Community.Application.CommunitySubscription.Caching;
using NetSpace.Community.Infrastructure.Community;
using NetSpace.Community.Infrastructure.CommunityPost;
using NetSpace.Community.Infrastructure.CommunityPostUserComment;
using NetSpace.Community.Infrastructure.CommunitySubscription;
using NetSpace.Community.Infrastructure.User;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.UseCases.Community;
using NetSpace.Community.UseCases.CommunityPost;
using NetSpace.Community.UseCases.CommunityPostUserComment;
using NetSpace.Community.UseCases.CommunitySubscription;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Infrastructure.Common.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString, string redisInstanceName, string redisConnectionString)
    {
        services.AddDbContext<NetSpaceDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });


        services.AddMassTransit(config =>
        {
            config.AddConsumers(typeof(UpdateCommunityCommand).Assembly);

            config.UsingRabbitMq((context, cfg) =>
            {
                //cfg.ReceiveEndpoint(e =>
                //{
                //    e.ConfigureConsumer<UserDeletedConsumer>(context);
                //});
            });
        });

        services.AddStackExchangeRedisCache(configure =>
        {
            configure.InstanceName = redisInstanceName;
            configure.Configuration = redisConnectionString;
        });

        services.AddScoped<ICommunityDistributedCache, CommunityDistributedCache>();
        services.AddScoped<ICommunityPostUserCommentDistributedCache, CommunityPostUserCommentDistributedCache>();
        services.AddScoped<ICommunityPostDistributedCache, CommunityPostDistributedCache>();
        services.AddScoped<ICommunitySubscriptionDistributedCache, CommunitySubscriptionDistributedCache>();

        services.AddScoped<ICommunityRepository, CommunityRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICommunityPostRepository, CommunityPostRepository>();
        services.AddScoped<ICommunityPostUserCommentRepository, CommunityPostUserCommentRepository>();
        services.AddScoped<ICommunitySubscriptionRepository, CommunitySubscriptionRepository>();

        services.AddScoped<ICommunityReadonlyRepository, CommunityRepository>();
        services.AddScoped<IUserReadonlyRepository, UserRepository>();
        services.AddScoped<ICommunityPostReadonlyRepository, CommunityPostRepository>();
        services.AddScoped<ICommunityPostUserCommentReadonlyRepository, CommunityPostUserCommentRepository>();
        services.AddScoped<ICommunitySubscriptionReadonlyRepository, CommunitySubscriptionRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IReadonlyUnitOfWork, ReadonlyUnitOfWork>();

        return services;
    }
}
