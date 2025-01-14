using MassTransit;
using Microsoft.AspNetCore.Identity;
using NetSpace.Common.Injector;
using NetSpace.Common.Messages.Email;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Infrastructure.Common.Email;

[Inject(ImplementationFor = typeof(IEmailSender<UserEntity>))]
public sealed class EmailSenderOfTUser(IPublishEndpoint publisher) : IEmailSender<UserEntity>
{
    public async Task SendConfirmationLinkAsync(UserEntity user, string email, string confirmationLink)
    {
        var msg = new SendEmailMessage { To = email, Subject = "Confirmation link", Body = confirmationLink };

        await publisher.Publish(msg);
    }

    public async Task SendPasswordResetCodeAsync(UserEntity user, string email, string resetCode)
    {
        var msg = new SendEmailMessage { To = email, Subject = "Reset password code", Body = resetCode };

        await publisher.Publish(msg);
    }

    public async Task SendPasswordResetLinkAsync(UserEntity user, string email, string resetLink)
    {
        var msg = new SendEmailMessage { To = email, Subject = "Reset password link", Body = resetLink };

        await publisher.Publish(msg);
    }
}
