using Microsoft.EntityFrameworkCore;
using NetSpace.Community.Domain.Community;
using NetSpace.Community.UseCases;
using NetSpace.Community.UseCases.Community;

namespace NetSpace.Community.Infrastructure.Community;

public sealed class CommunityRepository(NetSpaceDbContext dbContext) : RepositoryBase<CommunityEntity, int>(dbContext), ICommunityRepository
{
    public async Task<IEnumerable<CommunityEntity>?> FilterAsync(CommunityFilterOptions filter,
                                                                 PaginationOptions pagination,
                                                                 SortOptions sort,
                                                                 CancellationToken cancellationToken = default)
    {
        var query = DbContext.Communities.AsQueryable();

        if (filter.Id != default)
            query = query.Where(c => c.Id == filter.Id);

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(c => c.Name == filter.Name);

        if (!string.IsNullOrWhiteSpace(filter.Description))
            query = query.Where(c => c.Description == filter.Description);

        if (filter.CreatedAt is not null)
            query = query.Where(c => c.CreatedAt == filter.CreatedAt);

        if (filter.LastNameUpdatedAt is not null)
            query = query.Where(c => c.LastNameUpdatedAt == filter.LastNameUpdatedAt);

        if (filter.OwnerId is not null)
            query = query.Where(c => c.OwnerId == filter.OwnerId);

        if (filter.IncludeOwner)
            query = query.Include(c => c.Owner);

        if (filter.IncludeCommunityPosts)
            query = query.Include(c => c.CommunityPosts);

        if (filter.IncludeCommunitySubscribers)
            query = query.Include(c => c.CommunitySubscribers);

        query = sort.OrderByAscending switch
        {
            "Id" => query.OrderBy(u => u.Id),
            "UserName" => query.OrderBy(c => c.Name),
            "Description" => query.OrderBy(c => c.Description),
            "AvatarUrl" => query.OrderBy(c => c.AvatarUrl),
            "OwnerId" => query.OrderBy(c => c.OwnerId),
            "CreatedAt" => query.OrderBy(u => u.CreatedAt),
            "LastNameUpdatedAt" => query.OrderBy(c => c.LastNameUpdatedAt),
            _ => query.OrderBy(u => u.Id)
        };

        query = sort.OrderByDescending switch
        {
            "Id" => query.OrderByDescending(u => u.Id),
            "UserName" => query.OrderByDescending(c => c.Name),
            "Description" => query.OrderByDescending(c => c.Description),
            "AvatarUrl" => query.OrderByDescending(c => c.AvatarUrl),
            "OwnerId" => query.OrderByDescending(c => c.OwnerId),
            "CreatedAt" => query.OrderByDescending(u => u.CreatedAt),
            "LastNameUpdatedAt" => query.OrderByDescending(c => c.LastNameUpdatedAt),
            _ => query.OrderBy(u => u.Id)
        };

        query = query
            .Skip((pagination.PageCount - 1) * pagination.PageSize)
            .Take(pagination.PageSize);

        return await query.ToArrayAsync(cancellationToken);
    }
}
