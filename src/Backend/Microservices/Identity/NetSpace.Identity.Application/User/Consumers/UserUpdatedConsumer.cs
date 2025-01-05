using MapsterMapper;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using NetSpace.Common.Messages.User;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Application.User.Consumers;

public sealed class UserUpdatedConsumer(UserManager<UserEntity> userManager, IMapper mapper) : IConsumer<UserUpdatedMessage>
{
    public async Task Consume(ConsumeContext<UserUpdatedMessage> context)
    {
        var msg = context.Message;

        var userEntity = await userManager.FindByIdAsync(msg.Id.ToString());

        if (userEntity == null)
            return;

        userEntity = mapper.Map(msg, userEntity);

        await userManager.UpdateAsync(userEntity);
    }
}
