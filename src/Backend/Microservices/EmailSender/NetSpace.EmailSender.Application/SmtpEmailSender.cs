using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace NetSpace.EmailSender.Application;

public sealed class SmtpEmailSender(IOptions<EmailSenderOptions> optionsGeneric) : IEmailSender
{
    private readonly EmailSenderOptions options = optionsGeneric.Value;
    private readonly SmtpClient smtpClient = new();

    public async Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        using var msg = new MimeMessage();

        msg.From.Add(new MailboxAddress("no-reply-NetSpace", options.From));
        msg.To.Add(new MailboxAddress(to, to));
        msg.Subject = subject;
        msg.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = body
        };

        smtpClient.Connect(options.Host, options.Port, true, cancellationToken);
        smtpClient.Authenticate(options.From, options.Password, cancellationToken);

        await smtpClient.SendAsync(msg, cancellationToken);

        await smtpClient.DisconnectAsync(true, cancellationToken);
        smtpClient.Dispose();
    }
}
