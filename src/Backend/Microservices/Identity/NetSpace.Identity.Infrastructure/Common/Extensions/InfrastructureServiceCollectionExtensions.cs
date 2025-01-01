using Microsoft.Extensions.DependencyInjection;
using NetSpace.Identity.Domain.User;
using NetSpace.Identity.Infrastructure.User;
using NetSpace.Identity.UseCases.User;

namespace NetSpace.Identity.Infrastructure.Common.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddIdentityCore<UserEntity>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<NetSpaceDbContext>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
