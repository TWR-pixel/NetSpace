using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Community.Application.Community;
using NetSpace.Community.Application.Community.Requests;

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

        return services;
    }
}
