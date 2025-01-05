using MapsterMapper;
using NetSpace.Friendship.Application.User.Exceptions;
using NetSpace.Friendship.UseCases.Friendship;
using NetSpace.Friendship.UseCases.User;

namespace NetSpace.Friendship.Application.User.Requests;

public sealed record GetPossibleFriendsRequest : RequestBase<IEnumerable<UserResponse>>
{
    public required Guid FromId { get; set; }
}

public sealed class GetPossibleFriendsRequestHandler(IFriendshipRepository friendshipRepository,
                                                     IUserRepository userRepository,
                                                     IMapper mapper) : RequestHandlerBase<GetPossibleFriendsRequest, IEnumerable<UserResponse>>
{
    public override async Task<IEnumerable<UserResponse>> Handle(GetPossibleFriendsRequest request, CancellationToken cancellationToken)
    {
        var userFrom = await userRepository.FindByIdAsync(request.FromId, cancellationToken)
            ?? throw new UserNotFoundException(request.FromId);

        var result = await friendshipRepository.GetPossibleFriends(userFrom, cancellationToken);

        return mapper.Map<IEnumerable<UserResponse>>(result);
    }
}
