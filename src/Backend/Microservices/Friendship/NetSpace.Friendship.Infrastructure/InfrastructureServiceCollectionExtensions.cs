using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neo4j.Berries.OGM;

namespace NetSpace.Friendship.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddNeo4j<NetSpaceDbContext>(configuration, options =>
        {
            options.ConfigureFromAssemblies(typeof(NetSpaceDbContext).Assembly);
            options.EnableTimestamps();
        });

        return services;
    }
}
