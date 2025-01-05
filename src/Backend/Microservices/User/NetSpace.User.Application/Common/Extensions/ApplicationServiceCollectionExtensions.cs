using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.User.Application.User;
using System.Reflection;

namespace NetSpace.User.Application.Common.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(UserResponse).Assembly);
        });

        services.AddValidatorsFromAssembly(typeof(UserResponse).Assembly);

        var config = new TypeAdapterConfig();

        var registers = config.Scan(Assembly.GetAssembly(typeof(ResponseBase)) ?? Assembly.GetExecutingAssembly());
        config.Apply(registers);

        services.AddSingleton(config);
        services.AddSingleton<IMapper, ServiceMapper>();

        return services;
    }
}
