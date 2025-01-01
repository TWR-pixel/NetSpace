using NetSpace.User.Application.Common.Cache;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Application.User.Extensions;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Application.User.Requests;

public sealed record DeleteUserByIdRequest : RequestBase<UserResponse>
{
    public required Guid Id { get; set; }
}

public sealed class DeleteUserByIdRequestHandler(IUserRepository userRepository, IUserDistributedCacheStorage cache) : RequestHandlerBase<DeleteUserByIdRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(DeleteUserByIdRequest request, CancellationToken cancellationToken)
    {
        var userFromDb = await userRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserNotFoundException(request.Id);

        await userRepository.DeleteAsync(userFromDb, cancellationToken);
        await cache.RemoveByIdAsync(userFromDb.Id, cancellationToken);

        return userFromDb.ToResponse();
    }
}
