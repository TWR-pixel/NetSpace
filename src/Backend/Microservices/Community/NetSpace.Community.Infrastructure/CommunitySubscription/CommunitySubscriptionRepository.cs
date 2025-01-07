using Microsoft.EntityFrameworkCore;
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

    public async Task<CommunitySubscriptionEntity> GetWithDetails(int id, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.CommunitySubscriptions
            .Include(c => c.Community)
                .ThenInclude(c => c.Owner)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return entity;
    }
}
