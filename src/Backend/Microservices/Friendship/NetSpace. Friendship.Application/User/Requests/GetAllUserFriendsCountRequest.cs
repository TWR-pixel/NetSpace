
using NetSpace.Friendship.Application.User.Exceptions;
using NetSpace.Friendship.UseCases.Friendship;
using NetSpace.Friendship.UseCases.User;

namespace NetSpace.Friendship.Application.User.Requests;

public sealed record GetAllUserFriendsCountRequest : RequestBase<long>
{
    public required Guid Id { get; set; }
}

public sealed class GetAllUserFriendsCountRequestHandler(IFriendshipRepository friendshipRepository, IUserRepository userRepository) : RequestHandlerBase<GetAllUserFriendsCountRequest, long>
{
    public override async Task<long> Handle(GetAllUserFriendsCountRequest request, CancellationToken cancellationToken)
    {
        var userEntity = await userRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserNotFoundException(request.Id);

        var result = await friendshipRepository.FriendsCountById(userEntity, cancellationToken);

        return result;
    }
}
