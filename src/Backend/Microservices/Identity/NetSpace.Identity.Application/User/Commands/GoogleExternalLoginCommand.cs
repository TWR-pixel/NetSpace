using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using NetSpace.Identity.Domain.User;
using System.Security.Claims;

namespace NetSpace.Identity.Application.User.Commands;

public sealed record GoogleExternalLoginCommand : RequestBase<UserResponse>
{
    public required ClaimsPrincipal User { get; set; }
}

public sealed class GoogleExternalLoginCommandHandler(UserManager<UserEntity> userManager, IMapper mapper) : RequestHandlerBase<GoogleExternalLoginCommand, UserResponse>
{
    public async override Task<UserResponse> Handle(GoogleExternalLoginCommand request, CancellationToken cancellationToken)
    {
        var user = request.User;

        // ClaimTypes.Role. don't tuch this;
        var userEmail = user.FindFirst(c => c.Type == ClaimTypes.Email)?.Value ?? throw new UnauthorizedAccessException();
        var userName = user.FindFirst(c => c.Type == ClaimTypes.GivenName)?.Value;
        var userSurname = user.FindFirst(c => c.Type == ClaimTypes.Surname)?.Value ?? "default";
        var userNickname = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "default";
        var emailVerified = bool.Parse(user.FindFirst(c => c.Type == "email_verified")?.Value ?? "false");

        var userFromDb = await userManager.FindByEmailAsync(userEmail);

        if (userFromDb is null)
        {
            var newUserEntity = new UserEntity
            {
                Nickname = userNickname,
                UserName = userName,
                Surname = userSurname,
                Email = userEmail,
            };

            if (emailVerified)
            {
                var userToken = await userManager.GenerateEmailConfirmationTokenAsync(newUserEntity);
                await userManager.ConfirmEmailAsync(newUserEntity, userToken);
            }

            var result = await userManager.CreateAsync(newUserEntity);

            return mapper.Map<UserResponse>(newUserEntity);
        }

        userFromDb.UserName = userName;
        userFromDb.Surname = userSurname;
        userFromDb.Nickname = userNickname;
        userFromDb.LastLoginAt = DateTime.UtcNow;

        return mapper.Map<UserResponse>(userFromDb);
    }
}
