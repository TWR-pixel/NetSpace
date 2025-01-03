using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.User.Application.User;

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

        return services;
    }
}
