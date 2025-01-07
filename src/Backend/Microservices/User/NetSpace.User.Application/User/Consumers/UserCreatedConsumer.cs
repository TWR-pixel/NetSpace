using MapsterMapper;
using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.User.Domain.User;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.User.Consumers;

public sealed class UserCreatedConsumer(IUnitOfWork unitOfWork, IMapper mapper) : IConsumer<UserCreatedMessage>
{
    public async Task Consume(ConsumeContext<UserCreatedMessage> context)
    {
        await unitOfWork.Users.AddAsync(mapper.Map<UserEntity>(context.Message), context.CancellationToken);
        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
