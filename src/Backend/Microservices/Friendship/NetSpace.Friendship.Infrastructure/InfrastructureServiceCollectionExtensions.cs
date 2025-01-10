using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neo4jClient;
using NetSpace.Friendship.Infrastructure.User;
using NetSpace.Friendship.UseCases.User;

namespace NetSpace.Friendship.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationSection neo4jConfigSection)
    {
        var client = new BoltGraphClient(new Uri(neo4jConfigSection["Uri"] ?? "bolt://localhost:7687"), neo4jConfigSection["Username"], neo4jConfigSection["Password"]);
        client.ConnectAsync();

        services.AddSingleton<IGraphClient>(client);
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}