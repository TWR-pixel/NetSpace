using NetSpace.Community.Domain.CommunitySubscription;

namespace NetSpace.Community.UseCases.CommunitySubscription;

public interface ICommunitySubscriptionRepository : IRepository<CommunitySubscriptionEntity, int>, ICommunitySubscriptionReadonlyRepository
{
   
}
