using MapsterMapper;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using NetSpace.Common.Messages.User;
using NetSpace.Identity.Domain.User;
using System.Security.Claims;

namespace NetSpace.Identity.Application.User.Commands;

public sealed record GoogleExternalLoginCommand : RequestBase<UserResponse>
{
    public required ClaimsPrincipal User { get; set; }
}

public sealed class GoogleExternalLoginCommandHandler(UserManager<UserEntity> userManager, IMapper mapper, IPublishEndpoint publisher) : RequestHandlerBase<GoogleExternalLoginCommand, UserResponse>
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

        var userEntity = await userManager.FindByEmailAsync(userEmail);

        if (userEntity is null)
        {
            userEntity = new UserEntity
            {
                Nickname = userNickname,
                UserName = userName,
                Surname = userSurname,
                Email = userEmail,
            };

            if (emailVerified)
            {
                var userToken = await userManager.GenerateEmailConfirmationTokenAsync(userEntity);
                await userManager.ConfirmEmailAsync(userEntity, userToken);
            }

            var result = await userManager.CreateAsync(userEntity);

            return mapper.Map<UserResponse>(userEntity);
        }

        userEntity.UserName = userName;
        userEntity.Surname = userSurname;
        userEntity.Nickname = userNickname;
        userEntity.LastLoginAt = DateTime.UtcNow;

        await publisher.Publish(mapper.Map<UserUpdatedMessage>(userEntity), cancellationToken);

        return mapper.Map<UserResponse>(userEntity);
    }
}
