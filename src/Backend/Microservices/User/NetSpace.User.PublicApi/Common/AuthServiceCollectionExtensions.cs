using System.Security.Claims;

namespace NetSpace.User.PublicApi.Common;

public static class AuthServiceCollectionExtensions
{
    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(AuthConstants.AdminPolicy, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, UserRoles.Admin);
                policy.RequireAuthenticatedUser();
            })
            .AddPolicy(AuthConstants.ModeratorPolicy, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, UserRoles.Moderator);
                policy.RequireAuthenticatedUser();
            });

        return services;
    }
}
