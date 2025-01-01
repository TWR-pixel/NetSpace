using NetSpace.Friendship.Application.User.Exceptions;
using NetSpace.Friendship.UseCases.Friendship;
using NetSpace.Friendship.UseCases.User;

namespace NetSpace.Friendship.Application.User.Requests;

public sealed record GetAllUserFollowersCountRequest : RequestBase<GetAllUserFollowersCountResponse>
{
    public required Guid Id { get; set; }
}

public sealed record GetAllUserFollowersCountResponse : ResponseBase;

public sealed class GetAllUserFollowersCountRequestHandler(IFriendshipRepository friendshipRepository, IUserRepository userRepository) : RequestHandlerBase<GetAllUserFollowersCountRequest, GetAllUserFollowersCountResponse>
{
    public override async Task<GetAllUserFollowersCountResponse> Handle(GetAllUserFollowersCountRequest request, CancellationToken cancellationToken)
    {
        var userFrom = await userRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserNotFoundException(request.Id);

        var result = await friendshipRepository.FollowersCountById(userFrom, cancellationToken);

        throw new NotImplementedException();
    }
}
