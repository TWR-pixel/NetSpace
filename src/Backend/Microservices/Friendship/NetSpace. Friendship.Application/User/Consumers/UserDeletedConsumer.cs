using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Friendship.Domain;
using NetSpace.Friendship.UseCases;

namespace NetSpace.Friendship.Application.User.Consumers;

public class UserDeletedConsumer(IUserRepository userRepository) : IConsumer<UserDeletedMessage>
{
    public async Task Consume(ConsumeContext<UserDeletedMessage> context)
    {
        var userMessage = context.Message;
        var userEntity = new UserEntity(userMessage.Id, userMessage.Name, userMessage.Surname, userMessage.BirthDate, Gender.NotSet);

        await userRepository.DeleteAsync(userEntity); // do we need method saveChanges()?
    }
}
