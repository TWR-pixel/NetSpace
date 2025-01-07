using NetSpace.Community.Application.Common.Caching;
using NetSpace.Community.Domain.Community;

namespace NetSpace.Community.Application.Community.Caching;

public interface ICommunityDistributedCache : IDistributedCacheStorage<CommunityEntity, int>
{
}
