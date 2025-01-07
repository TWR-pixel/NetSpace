using Microsoft.EntityFrameworkCore;
using NetSpace.Community.Domain.CommunityPost;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.UseCases.CommunityPost;

namespace NetSpace.Community.Infrastructure.CommunityPost;

public sealed class CommunityPostRepository(NetSpaceDbContext dbContext) : RepositoryBase<CommunityPostEntity, int>(dbContext), ICommunityPostRepository
{

    public async Task<IEnumerable<CommunityPostEntity>> FilterAsync(CommunityPostFilterOptions filter, PaginationOptions pagination, SortOptions sort, CancellationToken cancellationToken = default)
    {
        var query = DbContext.CommunityPosts.AsQueryable();

        if (filter.Id is not null)
            query = query.Where(c => c.Id == filter.Id);

        if (!string.IsNullOrWhiteSpace(filter.Title))
            query = query.Where(c => c.Title == filter.Title);

        if (!string.IsNullOrWhiteSpace(filter.Body))
            query = query.Where(c => c.Body == filter.Body);

        if (filter.CommunityId is not null)
            query = query.Where(c => c.CommunityId == filter.CommunityId);

        if (filter.IncludeCommunity)
            query = query.Include(c => c.Community);

        query = sort.OrderByAscending switch
        {
            "Id" => query.OrderBy(u => u.Id),
            "Title" => query.OrderBy(c => c.Title),
            "Body" => query.OrderBy(c => c.Body),
            "CommunityId" => query.OrderBy(c => c.CommunityId),
            _ => query.OrderBy(u => u.Id)
        };

        query = sort.OrderByDescending switch
        {
            "Id" => query.OrderByDescending(u => u.Id),
            "Title" => query.OrderByDescending(c => c.Title),
            "Body" => query.OrderByDescending(c => c.Body),
            "CommunityId" => query.OrderByDescending(c => c.CommunityId),
            _ => query.OrderBy(u => u.Id)
        };

        query = query
            .Skip((pagination.PageCount - 1) * pagination.PageSize)
            .Take(pagination.PageSize);

        return await query.ToArrayAsync(cancellationToken);
    }

    public async Task<CommunityPostEntity?> GetByIdWithDetails(int id, CancellationToken cancellationToken = default)
    {
        var communityPostEntity = await DbContext.CommunityPosts
            .Include(c => c.Community)
                .ThenInclude(c => c.Owner)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return communityPostEntity;
    }

    public async Task<IEnumerable<CommunityPostEntity>> GetLatest(PaginationOptions pagination, CancellationToken cancellationToken = default)
    {
        var communityPostEntities = await DbContext.CommunityPosts
            .OrderBy(c => c.CreatedAt)
            .Skip((pagination.PageCount - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .Include(c => c.Community)
            .ToArrayAsync(cancellationToken);


        return communityPostEntities;
    }
}
