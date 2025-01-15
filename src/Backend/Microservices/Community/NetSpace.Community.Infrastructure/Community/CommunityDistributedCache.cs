using Microsoft.Extensions.Caching.Distributed;
using NetSpace.Common.Injector;
using NetSpace.Community.Application.Community.Caching;
using NetSpace.Community.Domain.Community;
using NetSpace.Community.Infrastructure.Common;

namespace NetSpace.Community.Infrastructure.Community;

[Inject(ImplementationsFor = [typeof(ICommunityDistributedCache)])]
public sealed class CommunityDistributedCache(IDistributedCache cache) : DistributedCacheBase<CommunityEntity, int>(cache), ICommunityDistributedCache
{
}
