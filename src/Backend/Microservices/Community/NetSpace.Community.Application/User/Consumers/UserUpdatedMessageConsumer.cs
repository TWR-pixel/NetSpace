using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Community.Domain.User;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Application.User.Consumers;

public sealed class UserUpdatedMessageConsumer(IUserRepository userRepository) : IConsumer<UserUpdatedMessage>
{
    public async Task Consume(ConsumeContext<UserUpdatedMessage> context)
    {
        var msg = context.Message;

        
    }
}
