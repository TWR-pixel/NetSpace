using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Friendship.Domain;
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

        await userRepository.AddAsync(userEntity, context.CancellationToken);
    }
}
