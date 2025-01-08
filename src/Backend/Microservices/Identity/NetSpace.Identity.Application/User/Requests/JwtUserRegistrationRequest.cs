using Microsoft.AspNetCore.Identity;
using NetSpace.Identity.Application.Common.Jwt;
using NetSpace.Identity.Application.User.Exceptions;
using NetSpace.Identity.Domain.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NetSpace.Identity.Application.User.Requests;

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
    public Gender Gender { get; set; } = Gender.NotSet;
    public Language Language { get; set; } = Language.NotSet;
    public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.NotSet;
}

public sealed class JwtUserRegistrationRequestHandler(UserManager<UserEntity> userManager,
                                                      RoleManager<IdentityRole> roleManager,
                                                      AccessTokenFactory tokenFactory) : RequestHandlerBase<JwtUserRegistrationRequest, AccessTokenResponse>
{
    public override async Task<AccessTokenResponse> Handle(JwtUserRegistrationRequest request, CancellationToken cancellationToken)
    {
        var userExists = await userManager.FindByEmailAsync(request.Email);
        if (userExists != null)
            throw new UserAlreadyExistsException(request.Email);

        var user = new UserEntity()
        {
            Email = request.Email,
            UserName = request.UserName,
            Surname = request.Surname,
            Nickname = request.Nickname,
            LastName = request.LastName,
            About = request.About,
            BirthDate = request.BirthDate,
            Gender = request.Gender,
            Language = request.Language,
            MaritalStatus = request.MaritalStatus
        };

        var createUserResult = await userManager.CreateAsync(user, request.Password);
        if (!createUserResult.Succeeded)
            throw new UnauthorizedAccessException(createUserResult.Errors.First().Description);

        if (!await roleManager.RoleExistsAsync(UserRoles.User))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        await userManager.AddToRoleAsync(user, UserRoles.User);

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

        var response = tokenFactory.GenerateToken(authClaims);

        return response;
    }
}