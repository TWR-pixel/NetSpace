using NetSpace.Friendship.Domain;
using NetSpace.Friendship.Domain.User;

namespace NetSpace.Friendship.UseCases.Friendship;

public interface IFriendshipRepository
{
    public Task CreateFriendship(UserEntity from, UserEntity to, FriendshipStatus status, CancellationToken cancellationToken = default);
    public Task<IEnumerable<UserEntity>> GetAllFollowersByStatus(UserEntity from, FriendshipStatus status, CancellationToken cancellationToken = default);
    public Task<IEnumerable<UserEntity>> GetAllFriendsByStatus(UserEntity from, FriendshipStatus status, CancellationToken cancellationToken = default);
    public Task<long> FriendsCountById(UserEntity from, CancellationToken cancellationToken = default);
    public Task<long> FollowersCountById(UserEntity from, CancellationToken cancellationToken = default);
    public Task UpdateFriendshipStatus(UserEntity from, UserEntity to, FriendshipStatus status, CancellationToken cancellationToken = default);
    public Task<bool> ExistsFriendshipWithStatus(UserEntity from, UserEntity to, FriendshipStatus status, CancellationToken cancellationToken = default);
}
