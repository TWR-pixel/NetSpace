using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Friendship.Domain;
using NetSpace.Friendship.UseCases.User;

namespace NetSpace.Friendship.Application.User.Consumers;

public sealed class UserUpdatedConsumer(IUserRepository users) : IConsumer<UserUpdatedMessage>
{
    public async Task Consume(ConsumeContext<UserUpdatedMessage> context)
    {
        var msg = context.Message;
        var userEntity = new UserEntity(msg.Id,
                                        msg.Nickname,
                                        msg.UserName,
                                        msg.Surname,
                                        msg.LastName,
                                        msg.About,
                                        msg.AvatarUrl,
                                        msg.BirthDate,
                                        msg.RegistrationDate,
                                        msg.LastLoginAt,
                                        (Domain.Gender)msg.Gender);

        await users.UpdateAsync(userEntity, context.CancellationToken);
    }
}
