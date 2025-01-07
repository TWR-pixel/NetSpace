using NetSpace.Community.Application.Common.Caching;
using NetSpace.Community.Domain.CommunityPost;

namespace NetSpace.Community.Application.CommunityPost.Caching;

public interface ICommunityPostDistributedCache : IDistributedCacheStorage<CommunityPostEntity, int>
{
    public Task<IEnumerable<CommunityPostEntity>?> GetLatest(CancellationToken cancellationToken);
    public Task SetLatest(IEnumerable<CommunityPostEntity> entities, CancellationToken cancellationToken);
}
