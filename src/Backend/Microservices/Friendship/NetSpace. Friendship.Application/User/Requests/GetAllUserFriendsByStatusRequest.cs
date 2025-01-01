using NetSpace.Friendship.Application.User.Exceptions;
using NetSpace.Friendship.Domain;
using NetSpace.Friendship.UseCases.Friendship;
using NetSpace.Friendship.UseCases.User;

namespace NetSpace.Friendship.Application.User.Requests;

public sealed record GetAllUserFriendsByStatusRequest : RequestBase<IEnumerable<UserResponse>>
{
    public required Guid Id { get; set; }
    public required FriendshipStatus Status { get; set; }
}

public sealed class GetAllUserFriendsByStatusRequestBase(IFriendshipRepository friendshipRepository, IUserRepository userRepository) : RequestHandlerBase<GetAllUserFriendsByStatusRequest, IEnumerable<UserResponse>>
{
    public override async Task<IEnumerable<UserResponse>> Handle(GetAllUserFriendsByStatusRequest request, CancellationToken cancellationToken)
    {
        var userFrom = await userRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserNotFoundException(request.Id);

        var allUserFriends = await friendshipRepository.GetAllFriendsByStatus(userFrom, request.Status, cancellationToken);

        var response = allUserFriends.Select(u => new UserResponse(u.Nickname, u.Name, u.Surname, u.LastName, u.About, u.AvatarUrl, u.BirthDate, u.Gender));

        return response;
    }
}