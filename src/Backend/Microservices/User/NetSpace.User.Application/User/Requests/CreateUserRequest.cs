using MassTransit;
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

        var userCreatedMessage = userEntity.ToUserCreated();

        await publisher.Publish(userCreatedMessage, cancellationToken);
        await cache.AddAsync(userEntity, cancellationToken);

        await userRepository.SaveChangesAsync(cancellationToken);

        return userEntity.ToResponse();
    }
}
