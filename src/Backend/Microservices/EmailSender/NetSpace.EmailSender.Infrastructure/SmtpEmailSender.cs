using NetSpace.EmailSender.UseCases;

namespace NetSpace.EmailSender.Infrastructure;

public sealed class SmtpEmailSender : IEmailSender
{
    public Task SendEmailAsync()
    {
        throw new NotImplementedException();
    }
}
