using NetSpace.Friendship.Application.User.Exceptions;
using NetSpace.Friendship.Domain;
using NetSpace.Friendship.UseCases.Friendship;
using NetSpace.Friendship.UseCases.User;

namespace NetSpace.Friendship.Application.User.Requests;

public sealed record UpdateFriendshipRequest : RequestBase<UpdateFriendshipResponse>
{
    public required Guid FromId { get; set; }
    public required Guid ToId { get; set; }
    public required FriendshipStatus Status { get; set; }
}

public sealed record UpdateFriendshipResponse : ResponseBase;

public sealed class UpdateFriendshipRequestHandler(IFriendshipRepository friendshipRepository, IUserRepository userRepository) : RequestHandlerBase<UpdateFriendshipRequest, UpdateFriendshipResponse>
{
    public override async Task<UpdateFriendshipResponse> Handle(UpdateFriendshipRequest request, CancellationToken cancellationToken)
    {
        var userFrom = await userRepository.FindByIdAsync(request.FromId, cancellationToken)
            ?? throw new UserNotFoundException(request.FromId);

        var userTo = await userRepository.FindByIdAsync(request.ToId, cancellationToken)
            ?? throw new UserNotFoundException(request.ToId);

        await friendshipRepository.UpdateFriendshipStatus(userFrom, userTo, request.Status, cancellationToken);

        return new UpdateFriendshipResponse();
    }
}
