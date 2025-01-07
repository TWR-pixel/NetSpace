using NetSpace.Community.Application.Common.Caching;
using NetSpace.Community.Domain.CommunitySubscription;

namespace NetSpace.Community.Application.CommunitySubscription.Caching;

public interface ICommunitySubscriptionDistributedCache : IDistributedCache<CommunitySubscriptionEntity, int>
{

}
