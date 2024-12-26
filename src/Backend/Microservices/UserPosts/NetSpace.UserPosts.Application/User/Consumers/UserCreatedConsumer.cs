using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.UserPosts.Domain;
using NetSpace.UserPosts.UseCases;

namespace NetSpace.UserPosts.Application.User.Consumers;

public sealed class UserCreatedConsumer(IUserRepository users) : IConsumer<UserCreatedMessage>
{
    public async Task Consume(ConsumeContext<UserCreatedMessage> context)
    {
        var msg = context.Message;
        var userEntity = new UserEntity(msg.Id, msg.Nickname, msg.Name, msg.Surname, msg.Email, msg.LastName, msg.About, msg.AvatarUrl, msg.BirthDate, (Domain.Gender)msg.Gender);
    
        await users.AddAsync(userEntity, context.CancellationToken);
    }
}
