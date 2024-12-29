namespace NetSpace.Community.Application.Common.MessageBroker;

public interface IPublisher
{
    public Task SendAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : class;
}
