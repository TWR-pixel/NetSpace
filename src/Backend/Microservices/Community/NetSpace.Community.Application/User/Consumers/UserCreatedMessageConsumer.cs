using MapsterMapper;
using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Community.Domain.User;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Application.User.Consumers;

public sealed class UserCreatedMessageConsumer(IUserRepository userRepository, IMapper mapper) : IConsumer<UserCreatedMessage>
{
    public async Task Consume(ConsumeContext<UserCreatedMessage> context)
    {
        var userEntity = await userRepository.FindByIdAsync(context.Message.Id, context.CancellationToken);

        if (userEntity != null)
            return;

        await userRepository.AddAsync(mapper.Map<UserEntity>(context.Message), context.CancellationToken);
        await userRepository.SaveChangesAsync(context.CancellationToken);
    }
}
