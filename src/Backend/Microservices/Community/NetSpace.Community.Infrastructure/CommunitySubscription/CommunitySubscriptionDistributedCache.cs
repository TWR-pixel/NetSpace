using Microsoft.Extensions.Caching.Distributed;
using NetSpace.Common.Injector;
using NetSpace.Community.Application.CommunitySubscription.Caching;
using NetSpace.Community.Domain.CommunitySubscription;
using NetSpace.Community.Infrastructure.Common;

namespace NetSpace.Community.Infrastructure.CommunitySubscription;

[Inject(ImplementationsFor = [typeof(ICommunitySubscriptionDistributedCache)])]
public sealed class CommunitySubscriptionDistributedCache(IDistributedCache cache)
    : DistributedCacheBase<CommunitySubscriptionEntity, int>(cache), ICommunitySubscriptionDistributedCache
{
}
