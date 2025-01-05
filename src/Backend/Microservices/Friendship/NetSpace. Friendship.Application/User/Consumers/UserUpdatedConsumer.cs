using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Friendship.Domain.User;
using NetSpace.Friendship.UseCases.User;

namespace NetSpace.Friendship.Application.User.Consumers;

public sealed class UserUpdatedConsumer(IUserRepository users) : IConsumer<UserUpdatedMessage>
{
    public async Task Consume(ConsumeContext<UserUpdatedMessage> context)
    {
        var msg = context.Message;
        var userEntity = new UserEntity
        {
            Email = msg.Email,
            Name = msg.UserName,
            Nickname = msg.Nickname,
            Surname = msg.Surname,
        };
        await users.UpdateAsync(userEntity, context.CancellationToken);
    }
}
