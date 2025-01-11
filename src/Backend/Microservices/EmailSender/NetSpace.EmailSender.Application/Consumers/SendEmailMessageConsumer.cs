using MassTransit;
using NetSpace.Common.Messages.Email;

namespace NetSpace.EmailSender.Application.Consumers;

public sealed class SendEmailMessageConsumer(IEmailSender emailSender) : IConsumer<SendEmailMessage>
{
    public async Task Consume(ConsumeContext<SendEmailMessage> context)
    {
        var msg = context.Message;

        await emailSender.SendAsync(msg.To, msg.Subject, msg.Body, context.CancellationToken);
    }
}
