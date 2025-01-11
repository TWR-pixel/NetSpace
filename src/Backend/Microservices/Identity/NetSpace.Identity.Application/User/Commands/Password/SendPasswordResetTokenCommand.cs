
using Microsoft.AspNetCore.Identity;
using NetSpace.Identity.Application.User.Exceptions;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Application.User.Commands.Password;

public sealed record SendPasswordResetTokenCommand : RequestBase<IdentityResult>
{
    public required Guid Id { get; set; }
}

public sealed class SendPasswordRecoveryCodeCommandHandler(UserManager<UserEntity> userManager, IEmailSender<UserEntity> emailSender)
    : RequestHandlerBase<SendPasswordResetTokenCommand, IdentityResult>
{
    public override async Task<IdentityResult> Handle(SendPasswordResetTokenCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await userManager.FindByIdAsync(request.Id.ToString())
            ?? throw new UserNotFoundException(request.Id);

        var token = await userManager.GeneratePasswordResetTokenAsync(userEntity);

        await emailSender.SendPasswordResetCodeAsync(userEntity, userEntity.Email!, token);

        return IdentityResult.Success;
    }
}