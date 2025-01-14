namespace NetSpace.User.PublicApi.Common;

public sealed class AuthConstants
{
    public const string AuthenticationSchemes = "Bearer, OpenIdConnect";
    public const string AdminPolicy = "RequiredAdminClaim";
    public const string ModeratorPolicy = "RequiredModeratorClaim";
}
