﻿namespace NetSpace.User.Application.Common.MessageBroker;

public interface IPublisher
{
    public Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : class;
}
