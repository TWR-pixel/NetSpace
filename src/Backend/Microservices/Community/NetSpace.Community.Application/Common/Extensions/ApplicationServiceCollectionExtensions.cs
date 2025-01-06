using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Community.Application.Community;
using NetSpace.Community.Application.Community.Commands.Update;
using NetSpace.Community.Application.CommunityPost;
using NetSpace.Community.Application.CommunityPostUserComment;
using NetSpace.Community.Application.User;

namespace NetSpace.Community.Application.Common.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(UpdateCommunityCommand).Assembly);
        });

        services.AddValidatorsFromAssembly(typeof(UpdateCommunityCommand).Assembly);

        services.AddSingleton(() =>
        {
            var config = new TypeAdapterConfig();
            new RegisterCommunityMapper().Register(config);
            new RegisterCommunityPostMapper().Register(config);
            new RegisterCommunitypostUserCommentMapper().Register(config);
            new RegisterUserMapper().Register(config);

            return config;
        });

        services.AddMapster();

        return services;
    }
}
