
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using NetSpace.Identity.Application.User.Exceptions;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Application.User.Commands.Password;

public sealed record ResetPasswordCommand : RequestBase<IdentityResult>
{
    public required string Email { get; set; }
    public required string NewPassword { get; set; }
    public required string Token { get; set; }
}

public sealed class ResetPasswordCommandHandler(UserManager<UserEntity> userManager, IMapper mapper) : RequestHandlerBase<ResetPasswordCommand, IdentityResult>
{
    public override async Task<IdentityResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await userManager.FindByEmailAsync(request.Email)
            ?? throw new UserNotFoundException(request.Email);

        var result = await userManager.ResetPasswordAsync(userEntity, request.Token, request.NewPassword);

        return result;
    }
}