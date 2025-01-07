using NetSpace.User.Application.Common.Cache;
using NetSpace.User.Domain.UserPost;

namespace NetSpace.User.Application.UserPost;

public interface IUserPostDistributedCacheStorage : IDistributedCacheStorage<UserPostEntity, int>
{
    public Task<IEnumerable<UserPostEntity>?> GetLatest(CancellationToken cancellationToken = default);
    public Task SetLatest(IEnumerable<UserPostEntity> entities, CancellationToken cancellationToken = default);
}
