using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Community.Application.Common.MediatR;
using NetSpace.Community.Application.Community.Commands;
using System.Reflection;

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

        services.AddMapsterAdapterConfig();
        services.AddMapster();

        return services;
    }

    public static IServiceCollection AddMapsterAdapterConfig(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();

        var registers = config.Scan(Assembly.GetAssembly(typeof(ResponseBase)) ?? Assembly.GetExecutingAssembly());
        config.Apply(registers);

        services.AddSingleton(config);

        return services;
    }
}
