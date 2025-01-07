using FluentValidation;
using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Identity.Application.User.Consumers;
using System.Reflection;

namespace NetSpace.Identity.Application.Common.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(RequestBase<>).Assembly);
        });

        services.AddValidatorsFromAssembly(typeof(RequestBase<>).Assembly);
        services.AddMapsterAdapterConfig();
        services.AddScoped<IMapper, ServiceMapper>();

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
