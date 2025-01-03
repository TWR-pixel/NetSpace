using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Community.Application.Community.Mappers;
using NetSpace.Community.Application.Community.Requests.Update;

namespace NetSpace.Community.Application.Common.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(UpdateCommunityRequest).Assembly);
        });

        services.AddValidatorsFromAssembly(typeof(UpdateCommunityRequest).Assembly);

        services.AddSingleton(() =>
        {
            var config = new TypeAdapterConfig();
            new RegisterCommunityMapper().Register(config);

            return config;
        });

        services.AddMapster();

        return services;
    }
}
