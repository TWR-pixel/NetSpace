using Microsoft.AspNetCore.Identity;
using NetSpace.Identity.Application.Common.Jwt;
using NetSpace.Identity.Application.User.Exceptions;
using NetSpace.Identity.Domain.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NetSpace.Identity.Application.User.Requests;

public sealed record JwtUserLoginRequest : RequestBase<AccessTokenResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public sealed class LoginUserRequestHandler(UserManager<UserEntity> userManager, AccessTokenFactory tokenFactory) : RequestHandlerBase<JwtUserLoginRequest, AccessTokenResponse>
{
    public override async Task<AccessTokenResponse> Handle(JwtUserLoginRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email) ?? throw new UserNotFoundException(request.Email);

        if (!await userManager.CheckPasswordAsync(user, request.Password))
            throw new NotRightPasswordException(request.Password);

        var userRoles = await userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
            {
               new(ClaimTypes.NameIdentifier, user.Id),
               new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = tokenFactory.GenerateToken(authClaims);

        return token;
    }
}
