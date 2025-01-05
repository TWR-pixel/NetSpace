using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Application.User.Consumers;

public sealed class UserDeletedMessageConsumer(IUserRepository userRepository) : IConsumer<UserDeletedMessage>
{
    public async Task Consume(ConsumeContext<UserDeletedMessage> context)
    {
        var userEntity = await userRepository.FindByIdAsync(context.Message.Id);

        if (userEntity == null)
            return;

        await userRepository.DeleteAsync(userEntity);
        await userRepository.SaveChangesAsync(context.CancellationToken);
    }
}
