using MassTransit;
using Microsoft.Extensions.Options;
using NetSpace.Common.Messages;

namespace NetSpace.EmailSender.Application.Consumers;

public sealed class SendEmailMessageConsumer(IEmailSender emailSender, IOptions<EmailSenderOptions> options) : IConsumer<SendEmailMessage>
{
    private readonly EmailSenderOptions value = options.Value;

    public async Task Consume(ConsumeContext<SendEmailMessage> context)
    {
        var msg = context.Message;

        await emailSender.SendAsync();
    }
}
