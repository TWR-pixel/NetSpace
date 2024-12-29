using MassTransit;
using NetSpace.Common.Messages.User;

namespace NetSpace.Community.Application.Community.Consumers;

public sealed class UserCreatedConsumer : IConsumer<UserCreatedMessage>
{
    public Task Consume(ConsumeContext<UserCreatedMessage> context)
    {
        throw new NotImplementedException();
    }
}
