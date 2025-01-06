using MapsterMapper;
using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Community.Domain.User;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.User.Consumers;

public sealed class UserCreatedMessageConsumer(IUnitOfWork unitOfWork, IMapper mapper) : IConsumer<UserCreatedMessage>
{
    public async Task Consume(ConsumeContext<UserCreatedMessage> context)
    {
        var userEntity = await unitOfWork.Users.FindByIdAsync(context.Message.Id, context.CancellationToken);

        if (userEntity != null)
            return;

        await unitOfWork.Users.AddAsync(mapper.Map<UserEntity>(context.Message), context.CancellationToken);
        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
