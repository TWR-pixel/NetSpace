using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.UserPosts.UseCases;

namespace NetSpace.UserPosts.Application.User.Consumers;

public sealed class UserDeletedConsumer(IUserRepository users) : IConsumer<UserDeletedMessage>
{
    public async Task Consume(ConsumeContext<UserDeletedMessage> context)
    {
        var userEntity = await users.FindByIdAsync(context.Message.Id, context.CancellationToken);

        if (userEntity != null)
        {
            await users.DeleteAsync(userEntity, context.CancellationToken);
        }
    }
}
