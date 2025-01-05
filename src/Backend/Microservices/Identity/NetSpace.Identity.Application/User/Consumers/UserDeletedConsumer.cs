using MapsterMapper;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using NetSpace.Common.Messages.User;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Application.User.Consumers;

public sealed class UserDeletedConsumer(UserManager<UserEntity> userManager, IMapper mapper) : IConsumer<UserDeletedMessage>
{
    public async Task Consume(ConsumeContext<UserDeletedMessage> context)
    {
        var msg = context.Message;

        var userEntity = await userManager.FindByIdAsync(msg.Id.ToString());

        if (userEntity is null)
            return;

        await userManager.DeleteAsync(userEntity);
    }
}
