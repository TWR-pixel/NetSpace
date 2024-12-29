using MassTransit;
using NetSpace.Common.Messages.User;

namespace NetSpace.Community.Application.Community.Consumers;

public sealed class UserDeletedConsumer : IConsumer<UserDeletedMessage>
{
    public Task Consume(ConsumeContext<UserDeletedMessage> context)
    {
        throw new NotImplementedException();
    }
}
