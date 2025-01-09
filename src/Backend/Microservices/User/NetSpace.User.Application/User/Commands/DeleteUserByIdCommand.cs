using MapsterMapper;
using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.User.Commands;

public sealed record DeleteUserByIdCommand : CommandBase<UserResponse>
{
    public required Guid Id { get; set; }
}

public sealed class DeleteUserByIdCommandHandler(IUnitOfWork unitOfWork,
                                                 IUserDistributedCacheStorage cache,
                                                 IMapper mapper,
                                                 IPublishEndpoint publisher) : CommandHandlerBase<DeleteUserByIdCommand, UserResponse>(unitOfWork)
{
    public override async Task<UserResponse> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await UnitOfWork.Users.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserNotFoundException(request.Id);

        await UnitOfWork.Users.DeleteAsync(userEntity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
        await cache.RemoveByIdAsync(userEntity.Id, cancellationToken);
        await publisher.Publish(mapper.Map<UserDeletedMessage>(userEntity), cancellationToken);

        return mapper.Map<UserResponse>(userEntity);
    }
}
