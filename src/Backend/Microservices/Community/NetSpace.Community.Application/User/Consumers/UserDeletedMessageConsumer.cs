using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Application.User.Consumers;

public sealed class UserDeletedMessageConsumer(IUnitOfWork unitOfWork) : IConsumer<UserDeletedMessage>
{
    public async Task Consume(ConsumeContext<UserDeletedMessage> context)
    {
        var userEntity = await unitOfWork.Users.FindByIdAsync(context.Message.Id);

        if (userEntity == null)
            return;

        await unitOfWork.Users.DeleteAsync(userEntity);
        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
