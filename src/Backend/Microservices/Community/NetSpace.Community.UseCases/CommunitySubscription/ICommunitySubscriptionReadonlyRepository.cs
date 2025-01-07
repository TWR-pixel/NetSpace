using NetSpace.Community.Domain.CommunitySubscription;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.UseCases.CommunitySubscription;

public interface ICommunitySubscriptionReadonlyRepository : IReadonlyRepository<CommunitySubscriptionEntity, int>
{
    public Task<IEnumerable<CommunitySubscriptionEntity>> Filter(CommunitySubscriptionFilterOptions filter,
                                                                 PaginationOptions pagination,
                                                                 SortOptions sort,
                                                                 CancellationToken cancellationToken = default);

    public Task<CommunitySubscriptionEntity> GetWithDetails(int id, CancellationToken cancellationToken = default);
}
