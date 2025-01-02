using MassTransit;
using NetSpace.Common.Messages.Email;
using NetSpace.User.Domain.User;

namespace NetSpace.User.Infrastructure.EmailSender;

public sealed class RabbitMQEmailPublisher(IPublishEndpoint publisher)
{
    public async Task SendConfirmationLinkAsync(UserEntity user, string email, string confirmationLink)
    {
        await SendAsync(user, email, "Confirmation link", confirmationLink);
    }

    public async Task SendPasswordResetCodeAsync(UserEntity user, string email, string resetCode)
    {
        await SendAsync(user, email, "Password reset code", resetCode);
    }

    public async Task SendPasswordResetLinkAsync(UserEntity user, string email, string resetLink)
    {
        await SendAsync(user, email, "Reset link", resetLink);
    }

    private async Task SendAsync(UserEntity user, string email, string subject, string body)
    {
        var mailMessage = new SendEmailMessage()
        {
            Body = body,
            To = email
        };

        await publisher.Publish(mailMessage);
    }

    //private async Task SendAsync(UserEntity user, string email,string subject , string body)
    //{
    //    var message = new MailMessage();
    //    message.To.Add(email);
    //    message.Subject = subject;
    //    message.Body = body;
    //    message.IsBodyHtml = true;
    //    message.From = new MailAddress(options.From);

    //    using var smtp = new SmtpClient(options.Host, options.Port);
    //    smtp.EnableSsl = true;
    //    smtp.Credentials = new NetworkCredential(options.UserName, options.Password);

    //    await smtp.SendMailAsync(message);
    //}
}
