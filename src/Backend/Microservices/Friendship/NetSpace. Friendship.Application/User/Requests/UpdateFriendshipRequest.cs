using NetSpace.Friendship.Domain;
using NetSpace.Friendship.UseCases;

namespace NetSpace.Friendship.Application.User.Requests;

public sealed record UpdateFriendshipRequest : RequestBase<UpdateFriendshipResponse>
{
    public required Guid FromId { get; set; }
    public required Guid ToId { get; set; }
    public required FriendshipStatus Status { get; set; }
}

public sealed record UpdateFriendshipResponse : ResponseBase;

public sealed class UpdateFriendshipRequestHandler(IUserRepository userRepository) : RequestHandlerBase<UpdateFriendshipRequest, UpdateFriendshipResponse>
{
    public override async Task<UpdateFriendshipResponse> Handle(UpdateFriendshipRequest request, CancellationToken cancellationToken)
    {
        await userRepository.UpdateFriendshipStatus(request.FromId.ToString(), request.ToId.ToString(), request.Status, cancellationToken);

        return new UpdateFriendshipResponse();
    }
}
