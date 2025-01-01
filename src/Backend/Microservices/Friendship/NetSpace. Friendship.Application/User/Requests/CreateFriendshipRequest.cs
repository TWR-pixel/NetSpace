using NetSpace.Friendship.Application.User.Exceptions;
using NetSpace.Friendship.UseCases.Friendship;
using NetSpace.Friendship.UseCases.User;

namespace NetSpace.Friendship.Application.User.Requests;

public sealed record CreateFriendshipRequest : RequestBase<CreateFriendshipResponse>
{
    /// <summary>
    /// User, creating a friendship
    /// </summary>
    public required Guid FromId { get; set; }

    /// <summary>
    /// User, accepting following friendship request
    /// </summary>
    public required Guid ToId { get; set; }
}

public sealed record CreateFriendshipResponse : ResponseBase
{

}

public sealed class CreateFriendshipRequestHandler(IFriendshipRepository friendshipRepository, IUserRepository userRepository) : RequestHandlerBase<CreateFriendshipRequest, CreateFriendshipResponse>
{
    public override async Task<CreateFriendshipResponse> Handle(CreateFriendshipRequest request, CancellationToken cancellationToken)
    {
        var userFrom = await userRepository.FindByIdAsync(request.FromId, cancellationToken)
            ?? throw new UserNotFoundException(request.FromId);

        var userTo = await userRepository.FindByIdAsync(request.ToId, cancellationToken)
            ?? throw new UserNotFoundException(request.ToId);

        var friendshipExists = await friendshipRepository.ExistsFriendshipWithStatus(userFrom, userTo, Domain.FriendshipStatus.Accepted, cancellationToken);

        if (friendshipExists)
            throw new FriendshipAlreadyExistsException(userFrom.Id, userTo.Id);

        await friendshipRepository.CreateFriendship(userFrom,
                                                    userTo,
                                                    Domain.FriendshipStatus.WaitingForConfirmation,
                                                    cancellationToken);

        return new CreateFriendshipResponse();
    }
}
