using MapsterMapper;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using NetSpace.Common.Messages.User;
using NetSpace.Identity.Application.Common.Jwt;
using NetSpace.Identity.Application.User.Exceptions;
using NetSpace.Identity.Domain.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NetSpace.Identity.Application.User.Queries;

public sealed record JwtUserLoginQuery : RequestBase<AccessTokenResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public sealed class LoginUserQueryHandler(UserManager<UserEntity> userManager,
                                          AccessTokenFactory tokenFactory,
                                          IPublishEndpoint publisher,
                                          IMapper mapper) : RequestHandlerBase<JwtUserLoginQuery, AccessTokenResponse>
{
    public override async Task<AccessTokenResponse> Handle(JwtUserLoginQuery request, CancellationToken cancellationToken)
    {
        var userEntity = await userManager.FindByEmailAsync(request.Email) ?? throw new UserNotFoundException(request.Email);

        if (!await userManager.CheckPasswordAsync(userEntity, request.Password))
            throw new NotRightPasswordException(request.Password);

        var userRoles = await userManager.GetRolesAsync(userEntity);
        var authClaims = new List<Claim>
            {
               new(ClaimTypes.NameIdentifier, userEntity.Id),
               new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = tokenFactory.GenerateToken(authClaims);

        userEntity.LastLoginAt = DateTime.UtcNow;
        await userManager.UpdateAsync(userEntity);
        await publisher.Publish(mapper.Map<UserUpdatedMessage>(userEntity), cancellationToken);

        return token;
    }
}
