using MapsterMapper;
using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Application.User.Consumers;

public sealed class UserUpdatedConsumer(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper) : IConsumer<UserUpdatedMessage>
{
    public async Task Consume(ConsumeContext<UserUpdatedMessage> context)
    {
        var msg = context.Message;
        var userEntity = await userRepository.FindByIdAsync(msg.Id, context.CancellationToken);

        if (userEntity is not null)
        {
            mapper.Map(msg, userEntity);

            await unitOfWork.SaveChangesAsync(context.CancellationToken);
        }

        return;
    }
}
