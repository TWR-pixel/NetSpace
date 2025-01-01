using NetSpace.Friendship.Domain;
using NetSpace.Friendship.UseCases;

namespace NetSpace.Friendship.Application.User.Requests;

public sealed record GetAllUserFollowersByStatusRequest : RequestBase<IEnumerable<UserEntity>>
{
    public required Guid Id { get; set; }
    public required FriendshipStatus Status { get; set; }
}

public sealed class GetAllUserFollowersByStatusRequestHandler(IUserRepository userRepository) : RequestHandlerBase<GetAllUserFollowersByStatusRequest, IEnumerable<UserEntity>>
{
    public override async Task<IEnumerable<UserEntity>> Handle(GetAllUserFollowersByStatusRequest request, CancellationToken cancellationToken)
    {
        var result = await userRepository.GetAllFollowersByStatus(request.Id, request.Status, cancellationToken);

        return result;
    }
}
