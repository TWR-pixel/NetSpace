using NetSpace.Common.UseCases;
using NetSpace.Friendship.Domain;

namespace NetSpace.Friendship.UseCases;

public interface IUserRepository : IRepository<UserEntity, Guid>
{
    public Task<IEnumerable<UserEntity>> GetAllRejectedFriendsById(Guid id, CancellationToken cancellationToken = default);
    public Task<IEnumerable<UserEntity>> GetAllAcceptedFriendsById(Guid id, CancellationToken cancellationToken = default);
    public Task<IEnumerable<UserEntity>> GetAllWaitingForConfirmationFriendsById(Guid id, CancellationToken cancellationToken);
    public Task CreateFriendship(Guid userIn, Guid userOut, CancellationToken cancellationToken = default);
    public Task<IEnumerable<UserEntity>> GetAllFriendsByStatus(Guid id, FriendshipStatus status, CancellationToken cancellationToken = default);
}
