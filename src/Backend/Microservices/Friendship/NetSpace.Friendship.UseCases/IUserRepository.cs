using NetSpace.Friendship.Domain;

namespace NetSpace.Friendship.UseCases;

public interface IUserRepository : IRepository<UserEntity, Guid>
{
    public Task CreateFriendship(Guid fromId, Guid toId, FriendshipStatus status, CancellationToken cancellationToken = default);
    public Task<IEnumerable<UserEntity>> GetAllFollowersByStatus(Guid id, FriendshipStatus status, CancellationToken cancellationToken = default);
    public Task<IEnumerable<UserEntity>> GetAllFriendsByStatus(Guid id, FriendshipStatus status, CancellationToken cancellationToken = default);
    public Task<long> FriendsCountById(Guid id, CancellationToken cancellationToken = default);
    public Task<long> FollowersCountById(Guid id, CancellationToken cancellationToken = default);
    public Task UpdateFriendshipStatus(Guid fromId, Guid toId, FriendshipStatus status, CancellationToken cancellationToken = default);
}
