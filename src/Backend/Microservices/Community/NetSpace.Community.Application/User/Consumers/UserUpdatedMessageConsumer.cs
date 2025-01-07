using MapsterMapper;
using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.User.Consumers;

public sealed class UserUpdatedMessageConsumer(IUnitOfWork unitOfWork, IMapper mapper) : IConsumer<UserUpdatedMessage>
{
    public async Task Consume(ConsumeContext<UserUpdatedMessage> context)
    {
        var msg = context.Message;

        var userEntity = await unitOfWork.Users.FindByIdAsync(msg.Id);

        if (userEntity is not null)
        {
            mapper.Map(msg, userEntity);

            await unitOfWork.SaveChangesAsync(context.CancellationToken);
        }
    }
}
