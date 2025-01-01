using NetSpace.EmailSender.Application;

namespace NetSpace.EmailSender.Infrastructure;

public sealed class SmtpEmailSender : IEmailSender
{
    public Task SendAsync()
    {
        throw new NotImplementedException();
    }
}
