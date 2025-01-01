using MassTransit;
using NetSpace.Common.Messages.User;

namespace NetSpace.Identity.Application.User.Consumers;

public sealed class UserDeletedConsumer : IConsumer<UserDeletedMessage>
{
    public Task Consume(ConsumeContext<UserDeletedMessage> context)
    {
        throw new NotImplementedException();
    }
}
