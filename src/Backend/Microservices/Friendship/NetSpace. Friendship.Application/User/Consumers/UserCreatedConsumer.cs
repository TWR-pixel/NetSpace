using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Friendship.Domain;
using NetSpace.Friendship.UseCases;

namespace NetSpace.Friendship.Application.User.Consumers;

public sealed class UserCreatedConsumer(IUserRepository users) : IConsumer<UserCreatedMessage>
{
    public async Task Consume(ConsumeContext<UserCreatedMessage> context)
    {
        var msg = context.Message;
        var userEntity = new UserEntity(msg.Id, msg.Nickname, msg.Name, msg.Surname, msg.LastName, msg.About, msg.AvatarUrl, msg.BirthDate, msg.RegistrationDate, msg.LastLoginAt, (Domain.Gender)msg.Gender);

        await users.AddAsync(userEntity, context.CancellationToken);
    }
}
