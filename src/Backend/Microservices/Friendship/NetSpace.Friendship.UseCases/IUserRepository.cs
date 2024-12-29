using NetSpace.Friendship.Domain;

namespace NetSpace.Friendship.UseCases;

public interface IUserRepository : IRepository<UserEntity, string>
{
    public Task CreateFriendship(string fromId, string toId, FriendshipStatus status, CancellationToken cancellationToken = default);
    public Task<IEnumerable<UserEntity>> GetAllFollowersByStatus(string id, FriendshipStatus status, CancellationToken cancellationToken = default);
    public Task<IEnumerable<UserEntity>> GetAllFriendsByStatus(string id, FriendshipStatus status, CancellationToken cancellationToken = default);
    public Task<long> FriendsCountById(string id, CancellationToken cancellationToken = default);
    public Task<long> FollowersCountById(string id, CancellationToken cancellationToken = default);
    public Task UpdateFriendshipStatus(string fromId, string toId, FriendshipStatus status, CancellationToken cancellationToken = default);
}
