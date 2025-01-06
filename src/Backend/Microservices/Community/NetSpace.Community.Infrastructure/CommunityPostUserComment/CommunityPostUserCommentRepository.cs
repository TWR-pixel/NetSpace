using Microsoft.EntityFrameworkCore;
using NetSpace.Community.Domain.CommunityPostUserComment;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.UseCases.CommunityPostUserComment;

namespace NetSpace.Community.Infrastructure.CommunityPostUserComment;

public sealed class CommunityPostUserCommentRepository(NetSpaceDbContext dbContext) : RepositoryBase<CommunityPostUserCommentEntity, int>(dbContext), ICommunityPostUserCommentRepository
{
    public async Task<IEnumerable<CommunityPostUserCommentEntity>?> FilterAsync(CommunityPostUsercommentFilterOptions filter, PaginationOptions pagination, SortOptions sort, CancellationToken cancellationToken = default)
    {
        var query = DbContext.CommunityPostUserComments.AsQueryable();

        if (filter.Id is not null)
            query = query.Where(c => c.Id == filter.Id);

        if (!string.IsNullOrWhiteSpace(filter.Body))
            query = query.Where(c => c.Body == filter.Body);

        if (filter.OwnerId is not null)
            query = query.Where(c => c.OwnerId == filter.OwnerId);

        if (filter.CommunityPostId is not null)
            query = query.Where(c => c.CommunityPostId == filter.CommunityPostId);

        if (filter.IncludeOwner)
            query = query.Include(c => c.Owner);

        if (filter.IncludeCommunityPost)
            query = query.Include(c => c.CommunityPost);

        query = sort.OrderByAscending switch
        {
            "Id" => query.OrderBy(u => u.Id),
            "Body" => query.OrderBy(c => c.Body),
            "OwnerId" => query.OrderBy(c => c.OwnerId),
            "CommunityPostId" => query.OrderBy(c => c.CommunityPostId),
            _ => query.OrderBy(u => u.Id)
        };

        query = sort.OrderByDescending switch
        {
            "Id" => query.OrderBy(u => u.Id),
            "Body" => query.OrderBy(c => c.Body),
            "OwnerId" => query.OrderBy(c => c.OwnerId),
            "CommunityPostId" => query.OrderBy(c => c.CommunityPostId),
            _ => query.OrderBy(u => u.Id)
        };

        query = query
            .Skip((pagination.PageCount - 1) * pagination.PageSize)
            .Take(pagination.PageSize);

        return await query.ToArrayAsync(cancellationToken);
    }
}
