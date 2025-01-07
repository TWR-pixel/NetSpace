using Microsoft.AspNetCore.Identity;
using NetSpace.Identity.Application.User.Exceptions;
using NetSpace.Identity.Domain.User;
using System.Security.Claims;
using System.Security.Cryptography;

namespace NetSpace.Identity.Application.User.Commands;

public sealed record GoogleExternalLoginCommand : RequestBase<UserResponse>
{
    public required ClaimsPrincipal User { get; set; }
}

public sealed class GoogleExternalLoginCommandHandler(UserManager<UserEntity> userManager) : RequestHandlerBase<GoogleExternalLoginCommand, UserResponse>
{
    public async override Task<UserResponse> Handle(GoogleExternalLoginCommand request, CancellationToken cancellationToken)
    {
        var user = request.User;

        if (user?.Identity != null && user.Identity.IsAuthenticated)
        {
            var userEmail = user.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            var userName = user.FindFirst("name")?.Value;
            var userSurname = "";
            var userNickname = RandomNumberGenerator.GetHexString(20);

            var userEntity = await userManager.FindByEmailAsync(userEmail);

            if (userEntity != null)
                throw new UserAlreadyExistsException(userEmail);

            return new UserResponse
            {
                UserName = userName,
                Email = userEmail,
                Surname = userSurname,
                Nickname = userNickname
            };
        }

        throw new UnauthorizedAccessException();
    }
}
