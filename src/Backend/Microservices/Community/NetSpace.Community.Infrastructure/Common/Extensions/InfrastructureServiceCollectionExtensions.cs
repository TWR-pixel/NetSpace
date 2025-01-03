using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Community.Infrastructure.Community;
using NetSpace.Community.Infrastructure.User;
using NetSpace.Community.UseCases.Community;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Infrastructure.Common.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<NetSpaceDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<ICommunityRepository, CommunityRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
