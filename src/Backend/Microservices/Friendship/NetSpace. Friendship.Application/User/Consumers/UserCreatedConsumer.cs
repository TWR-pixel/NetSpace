using MapsterMapper;
using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Friendship.Domain.User;
using NetSpace.Friendship.UseCases.User;

namespace NetSpace.Friendship.Application.User.Consumers;

public sealed class UserCreatedConsumer(IUserRepository userRepository, IMapper mapper) : IConsumer<UserCreatedMessage>
{
    public async Task Consume(ConsumeContext<UserCreatedMessage> context)
    {
        var msg = context.Message;
        var user = await userRepository.FindByIdAsync(msg.Id);

        if (user == null)
            return;

        var userEntity = mapper.Map<UserEntity>(msg);

        await userRepository.AddAsync(userEntity, context.CancellationToken);
    }
}
