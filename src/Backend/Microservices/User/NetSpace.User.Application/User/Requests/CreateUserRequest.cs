using MassTransit;
using Microsoft.AspNetCore.Identity;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.User.Extensions;
using NetSpace.User.Domain.User;

namespace NetSpace.User.Application.User.Requests;

public sealed class CreateUserRequestHandler(IPublishEndpoint publisher, UserManager<UserEntity> userManager) : RequestHandlerBase<UserRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(UserRequest request, CancellationToken cancellationToken)
    {
        var userEntity = request.ToEntity();
        await userManager.CreateAsync(userEntity, request.Password);

        var userCreatedMessage = new UserCreatedMessage(userEntity.Id,
                                                        userEntity.Nickname,
                                                        userEntity.Name,
                                                        userEntity.Surname,
                                                        userEntity.Email,
                                                        userEntity.LastName,
                                                        userEntity.About,
                                                        userEntity.AvatarUrl,
                                                        (NetSpace.Common.Messages.User.Gender)userEntity.Gender);

        await publisher.Publish(userCreatedMessage, cancellationToken);

        return userEntity.ToResponse();
    }
}
