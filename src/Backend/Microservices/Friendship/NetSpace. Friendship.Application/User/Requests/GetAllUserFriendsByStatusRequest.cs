using NetSpace.Friendship.Domain;
using NetSpace.Friendship.UseCases;

namespace NetSpace.Friendship.Application.User.Requests;

public sealed record GetAllUserFriendsByStatusRequest : RequestBase<IEnumerable<UserResponse>>
{
    public required Guid Id { get; set; }
    public required FriendshipStatus Status { get; set; }
}

public sealed class GetAllUserFriendsByStatusRequestBase(IUserRepository userRepository) : RequestHandlerBase<GetAllUserFriendsByStatusRequest, IEnumerable<UserResponse>>
{
    public override async Task<IEnumerable<UserResponse>> Handle(GetAllUserFriendsByStatusRequest request, CancellationToken cancellationToken)
    {
        var allUserFriends = await userRepository.GetAllFriendsByStatus(request.Id.ToString(), request.Status, cancellationToken);

        var response = allUserFriends.Select(u => new UserResponse(u.Nickname, u.Name, u.Surname, u.LastName, u.About, u.AvatarUrl, u.BirthDate, u.Gender));

        return response;
    }
}