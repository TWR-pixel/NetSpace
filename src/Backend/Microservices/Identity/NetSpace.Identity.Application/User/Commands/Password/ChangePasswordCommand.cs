using Microsoft.AspNetCore.Identity;
using NetSpace.Identity.Application.User.Exceptions;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Application.User.Commands.Password;

public sealed record ChangePasswordCommand : RequestBase<IdentityResult>
{
    public required string Email { get; set; }
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}

public sealed class ChangePasswordCommandHandler(UserManager<UserEntity> userManager) : RequestHandlerBase<ChangePasswordCommand, IdentityResult>
{
    public override async Task<IdentityResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await userManager.FindByEmailAsync(request.Email)
            ?? throw new UserNotFoundException(request.Email);

        var result = await userManager.ChangePasswordAsync(userEntity, request.CurrentPassword, request.NewPassword);

        return result;
    }
}
