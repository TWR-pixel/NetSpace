using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.Common.Cache;
using NetSpace.User.Application.User.Extensions;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Application.User.Requests;

public sealed class CreateUserRequestHandler(IPublishEndpoint publisher,
                                             IUserRepository userRepository,
                                             IUserDistributedCacheStorage cache) : RequestHandlerBase<UserRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(UserRequest request, CancellationToken cancellationToken)
    {
        var userEntity = request.ToEntity();
        await userRepository.AddAsync(userEntity, cancellationToken);

        var userCreatedMessage = new UserCreatedMessage(userEntity.Id,
                                                        userEntity.Nickname,
                                                        userEntity.Name,
                                                        userEntity.Surname,
                                                        userEntity.Email,
                                                        userEntity.LastName,
                                                        userEntity.About,
                                                        userEntity.AvatarUrl,
                                                        (Gender)userEntity.Gender);

        await publisher.Publish(userCreatedMessage, cancellationToken);
        await cache.AddAsync(userEntity, cancellationToken);

        return userEntity.ToResponse();
    }
}
