using MassTransit;
using NetSpace.User.Application.Common.MessageBroker;

namespace NetSpace.User.Infrastructure;

public sealed class RabbitMQPublisher(IPublishEndpoint publishEndpoint) : IPublisher
{
    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : class
    {
        await publishEndpoint.Publish(message, cancellationToken);
    }
}
