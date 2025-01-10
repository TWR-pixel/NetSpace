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

public sealed record JwtUserRegistrationRequest : RequestBase<AccessTokenResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Nickname { get; set; }
    public required string UserName { get; set; }
    public required string Surname { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;

    public DateTime? BirthDate { get; set; }
    public Domain.User.Gender Gender { get; set; } = Domain.User.Gender.NotSet;
    public Domain.User.Language Language { get; set; } = Domain.User.Language.NotSet;
    public Domain.User.MaritalStatus MaritalStatus { get; set; } = Domain.User.MaritalStatus.NotSet;
}

public sealed class JwtUserRegistrationRequestHandler(UserManager<UserEntity> userManager,
                                                      RoleManager<IdentityRole> roleManager,
                                                      AccessTokenFactory tokenFactory,
                                                      IPublishEndpoint publisher,
                                                      IMapper mapper) : RequestHandlerBase<JwtUserRegistrationRequest, AccessTokenResponse>
{
    public override async Task<AccessTokenResponse> Handle(JwtUserRegistrationRequest request, CancellationToken cancellationToken)
    {
        var userEntity = await userManager.FindByEmailAsync(request.Email);
        if (userEntity != null)
            throw new UserAlreadyExistsException(request.Email);

        userEntity = mapper.Map<UserEntity>(request);

        var createUserResult = await userManager.CreateAsync(userEntity, request.Password);
        if (!createUserResult.Succeeded)
            throw new UnauthorizedAccessException(createUserResult.Errors.First().Description);

        if (!await roleManager.RoleExistsAsync(UserRoles.User))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        await userManager.AddToRoleAsync(userEntity, UserRoles.User);

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

        var response = tokenFactory.GenerateToken(authClaims);

        await publisher.Publish(mapper.Map<UserCreatedMessage>(userEntity), cancellationToken);

        return response;
    }
}