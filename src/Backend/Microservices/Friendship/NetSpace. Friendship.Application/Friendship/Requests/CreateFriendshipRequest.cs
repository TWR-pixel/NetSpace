using NetSpace.Common.Application;
using NetSpace.Friendship.UseCases;

namespace NetSpace.Friendship.Application.Friendship.Requests;

public sealed record CreateFriendshipRequest : RequestBase<CreateFriendshipResponse>
{
    /// <summary>
    /// User, creating a friendship
    /// </summary>
    public required Guid FollowingId { get; set; }

    /// <summary>
    /// User, accepting following friendship request
    /// </summary>
    public required Guid FollowerId { get; set; }
}

public sealed record CreateFriendshipResponse : ResponseBase
{

}

public sealed class CreateFriendshipRequestHandler(IUserRepository userRepository) : RequestHandlerBase<CreateFriendshipRequest, CreateFriendshipResponse>
{
    public override async Task<CreateFriendshipResponse> Handle(CreateFriendshipRequest request, CancellationToken cancellationToken)
    {
        var following = await userRepository.FindByIdAsync(request.FollowingId, cancellationToken);
        var follower = await userRepository.FindByIdAsync(request.FollowerId, cancellationToken);

        throw new NotImplementedException();
    }
}
