using MapsterMapper;
using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Friendship.UseCases.User;

namespace NetSpace.Friendship.Application.User.Consumers;

public sealed class UserUpdatedConsumer(IUserRepository users, IMapper mapper) : IConsumer<UserUpdatedMessage>
{
    public async Task Consume(ConsumeContext<UserUpdatedMessage> context)
    {
        var msg = context.Message;
        var userEntity = await users.FindByIdAsync(msg.Id, context.CancellationToken);

        if (userEntity != null)
        {

            mapper.Map(msg, userEntity);

            await users.UpdateAsync(userEntity, context.CancellationToken);
        }
    }
}
