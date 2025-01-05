using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Friendship.Domain.User;
using NetSpace.Friendship.UseCases.User;

namespace NetSpace.Friendship.Application.User.Consumers;

public sealed class UserCreatedConsumer(IUserRepository userRepository) : IConsumer<UserCreatedMessage>
{
    public async Task Consume(ConsumeContext<UserCreatedMessage> context)
    {
        var msg = context.Message;
        var user = await userRepository.FindByIdAsync(msg.Id);
        if (user == null)
            return;

        var userEntity = new UserEntity
        {
            Email = msg.Email,
            Name = msg.UserName,
            Nickname = msg.Nickname,
            Surname = msg.Surname,
        };

        await userRepository.AddAsync(userEntity, context.CancellationToken);
    }
}
