using MassTransit;
using NetSpace.Common.Messages.User;

namespace NetSpace.UserPosts.Application.User.Consumers;

public sealed class UserDeletedConsumer : IConsumer<UserDeletedConsumer>
{
    public Task Consume(ConsumeContext<UserDeletedConsumer> context)
    {
        throw new NotImplementedException();
    }
}
