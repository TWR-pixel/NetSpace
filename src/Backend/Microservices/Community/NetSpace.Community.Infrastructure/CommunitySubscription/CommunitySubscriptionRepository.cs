using NetSpace.Community.Domain.CommunitySubscription;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.UseCases.CommunitySubscription;

namespace NetSpace.Community.Infrastructure.CommunitySubscription;

public sealed class CommunitySubscriptionRepository(NetSpaceDbContext dbContext) : RepositoryBase<CommunitySubscriptionEntity, int>(dbContext), ICommunitySubscriptionRepository
{
    public Task<IEnumerable<CommunitySubscriptionEntity>> Filter(CommunitySubscriptionFilterOptions filter,
                                                                 PaginationOptions pagination,
                                                                 SortOptions sort,
                                                                 CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
