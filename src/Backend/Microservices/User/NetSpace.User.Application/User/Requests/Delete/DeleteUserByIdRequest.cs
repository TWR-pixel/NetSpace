using MapsterMapper;
using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.Common.Cache;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Application.User.Requests.Delete;

public sealed record DeleteUserByIdRequest : RequestBase<UserResponse>
{
    public required Guid Id { get; set; }
}

public sealed class DeleteUserByIdRequestHandler(IUserRepository userRepository,
                                                 IUserDistributedCacheStorage cache,
                                                 IMapper mapper,
                                                 IPublishEndpoint publisher) : RequestHandlerBase<DeleteUserByIdRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(DeleteUserByIdRequest request, CancellationToken cancellationToken)
    {
        var userEntity = await userRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserNotFoundException(request.Id);

        await userRepository.DeleteAsync(userEntity, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);
        await cache.RemoveByIdAsync(userEntity.Id, cancellationToken);
        await publisher.Publish(mapper.Map<UserDeletedMessage>(userEntity));


        return mapper.Map<UserResponse>(userEntity);
    }
}
