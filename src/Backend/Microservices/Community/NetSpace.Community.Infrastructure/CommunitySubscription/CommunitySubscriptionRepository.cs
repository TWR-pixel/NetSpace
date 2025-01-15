using Microsoft.EntityFrameworkCore;
using NetSpace.Common.Injector;
using NetSpace.Community.Domain.CommunitySubscription;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.UseCases.CommunitySubscription;

namespace NetSpace.Community.Infrastructure.CommunitySubscription;

[Inject(ImplementationsFor = [typeof(ICommunitySubscriptionRepository), typeof(ICommunitySubscriptionReadonlyRepository)])]
public sealed class CommunitySubscriptionRepository(NetSpaceDbContext dbContext) : RepositoryBase<CommunitySubscriptionEntity, int>(dbContext), ICommunitySubscriptionRepository
{
    public async Task<IEnumerable<CommunitySubscriptionEntity>> Filter(CommunitySubscriptionFilterOptions filter,
                                                                 PaginationOptions pagination,
                                                                 SortOptions sort,
                                                                 CancellationToken cancellationToken = default)
    {
        var query = DbContext.CommunitySubscriptions.AsQueryable();

        if (filter.Id is not null)
            query = query.Where(c => c.Id == filter.Id);

        if (filter.SubscriberId is not null)
            query = query.Where(c => c.SubscriberId == filter.SubscriberId);

        if (filter.IncludeSubscriber)
            query = query.Include(c => c.Subscriber);

        if (filter.CommunityId is not null)
            query = query.Where(c => c.CommunityId == filter.CommunityId);

        if (filter.IncludeCommunity)
            query = query.Include(c => c.Community);

        if (filter.SubscribedAt is not null)
            query = query.Where(c => c.SubscribedAt == filter.SubscribedAt);

        if (filter.SubscribingStatus is not null)
            query = query.Where(c => c.SubscribingStatus == filter.SubscribingStatus);

        query = sort.OrderByAscending switch
        {
            "Id" => query.OrderBy(u => u.Id),
            "SubscriberId" => query.OrderBy(c => c.SubscriberId),
            "CommunityId" => query.OrderBy(c => c.CommunityId),
            "SubscribedAt" => query.OrderBy(c => c.SubscribedAt),
            "SubscribingStatus" => query.OrderBy(c => c.SubscribingStatus),
            _ => query.OrderBy(u => u.Id)
        };

        query = sort.OrderByDescending switch
        {
            "Id" => query.OrderByDescending(u => u.Id),
            "SubscriberId" => query.OrderByDescending(c => c.SubscriberId),
            "CommunityId" => query.OrderByDescending(c => c.CommunityId),
            "SubscribedAt" => query.OrderByDescending(c => c.SubscribedAt),
            "SubscribingStatus" => query.OrderByDescending(c => c.SubscribingStatus),
            _ => query.OrderBy(u => u.Id)
        };

        query = query
            .Skip((pagination.PageCount - 1) * pagination.PageSize)
            .Take(pagination.PageSize);

        return await query.ToArrayAsync(cancellationToken);
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
