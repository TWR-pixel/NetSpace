using MassTransit;
using Microsoft.AspNetCore.Identity.UI.Services;
using NetSpace.Common.Injector;
using NetSpace.Common.Messages.Email;

namespace NetSpace.Identity.Infrastructure.Common.Email;

[Inject(ImplementationsFor = [typeof(IEmailSender)])]
public sealed class EmailSender(IPublishEndpoint publisher) : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var msg = new SendEmailMessage { To = email, Subject = subject, Body = htmlMessage };

        await publisher.Publish(msg);
    }
}
