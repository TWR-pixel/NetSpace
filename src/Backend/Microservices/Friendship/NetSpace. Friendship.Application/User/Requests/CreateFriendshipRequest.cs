using NetSpace.Common.Application;
using NetSpace.Friendship.UseCases;

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

public sealed class CreateFriendshipRequestHandler(IUserRepository userRepository) : RequestHandlerBase<CreateFriendshipRequest, CreateFriendshipResponse>
{
    public override async Task<CreateFriendshipResponse> Handle(CreateFriendshipRequest request, CancellationToken cancellationToken)
    {
        await userRepository.CreateFriendship(request.FromId.ToString(),
                                              request.ToId.ToString(),
                                              Domain.FriendshipStatus.WaitingForConfirmation,
                                              cancellationToken);

        return new CreateFriendshipResponse();
    }
}
