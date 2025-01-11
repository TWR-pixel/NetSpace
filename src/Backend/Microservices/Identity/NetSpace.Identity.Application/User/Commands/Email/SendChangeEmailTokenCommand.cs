using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using NetSpace.Identity.Application.User.Exceptions;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Application.User.Commands.Email;

public sealed record SendChangeEmailTokenCommand : RequestBase<IdentityResult>
{
    public required string Email { get; set; }
    public required string NewEmail { get; set; }
}

public sealed class SendChangeEmailTokenCommandHandler(UserManager<UserEntity> userManager, IEmailSender emailSender) : RequestHandlerBase<SendChangeEmailTokenCommand, IdentityResult>
{
    public override async Task<IdentityResult> Handle(SendChangeEmailTokenCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await userManager.FindByEmailAsync(request.Email)
            ?? throw new UserNotFoundException(request.Email);

        var result = await userManager.GenerateChangeEmailTokenAsync(userEntity, request.NewEmail);
        await emailSender.SendEmailAsync(request.NewEmail, "Change email token", result);

        return IdentityResult.Success;
    }
}
