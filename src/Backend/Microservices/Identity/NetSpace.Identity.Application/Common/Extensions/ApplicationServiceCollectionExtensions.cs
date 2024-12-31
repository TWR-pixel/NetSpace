using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

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

        return services;
    }
}
