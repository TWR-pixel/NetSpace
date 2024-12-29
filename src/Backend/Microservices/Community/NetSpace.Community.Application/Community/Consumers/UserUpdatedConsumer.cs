using MassTransit;
using NetSpace.Common.Messages.User;

namespace NetSpace.Community.Application.Community.Consumers;

public sealed class UserUpdatedConsumer : IConsumer<UserUpdatedMessage>
{
    public Task Consume(ConsumeContext<UserUpdatedMessage> context)
    {
        throw new NotImplementedException();
    }
}
