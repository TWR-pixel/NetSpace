using NetSpace.Friendship.Application.User.Exceptions;
using NetSpace.Friendship.Domain;
using NetSpace.Friendship.Domain.User;
using NetSpace.Friendship.UseCases.Friendship;
using NetSpace.Friendship.UseCases.User;

namespace NetSpace.Friendship.Application.User.Requests;

public sealed record GetAllUserFollowersByStatusRequest : RequestBase<IEnumerable<UserEntity>>
{
    public required Guid Id { get; set; }
    public required FriendshipStatus Status { get; set; }
}

public sealed class GetAllUserFollowersByStatusRequestHandler(IFriendshipRepository friendshipRepository, IUserRepository userRepository) : RequestHandlerBase<GetAllUserFollowersByStatusRequest, IEnumerable<UserEntity>>
{
    public override async Task<IEnumerable<UserEntity>> Handle(GetAllUserFollowersByStatusRequest request, CancellationToken cancellationToken)
    {
        var userFrom = await userRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserNotFoundException(request.Id);

        var result = await friendshipRepository.GetAllFollowersByStatus(userFrom, request.Status, cancellationToken);

        return result;
    }
}
