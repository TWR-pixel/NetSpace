using Microsoft.EntityFrameworkCore;
using NetSpace.User.Domain.User;
using NetSpace.User.UseCases;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.Infrastructure.UserPostUserComment;

public sealed class UserPostUserCommentRepository(NetSpaceDbContext dbContext) : RepositoryBase<UserPostUserCommentEntity, int>(dbContext), IUserPostUserCommentRepository
{
    public async Task<IEnumerable<UserPostUserCommentEntity>> Filter(UserPostUserCommentFilterOptions filter,
                                                         PaginationOptions pagination,
                                                         SortOptions sort,
                                                         CancellationToken cancellationToken = default)
    {
        var query = DbContext.UserPostUserComments.AsQueryable();

        if (filter.Id != null)
            query = query.Where(u => u.Id == filter.Id);

        if (filter.CreatedAt != null)
            query = query.Where(u => u.CreatedAt == filter.CreatedAt);

        if (!string.IsNullOrWhiteSpace(filter.Body))
            query = query.Where(u => u.Body == filter.Body);

        if (filter.OwnerId != null)
            query = query
                .Where(u => u.UserId == filter.OwnerId)
                .Include(u => u.Owner);

        if (filter.UserPostId != null)
            query = query
                .Where(u => u.UserPostId == filter.UserPostId)
                .Include(u => u.UserPost);

        if (filter.IncludeUserPost == true)
        {
            query = query
                .Include(u => u.UserPost);
        }

        if (filter.IncludeOwner == true)
        {
            query = query
                .Include(u => u.Owner);
        }

        query = query
            .Skip((pagination.PageCount - 1) * pagination.PageSize)
            .Take(pagination.PageSize);

        query = sort.OrderByAscending switch
        {
            "Id" => query.OrderBy(u => u.Id),
            "CreatedAt" => query.OrderBy(u => u.CreatedAt),
            _ => query.OrderBy(u => u.Id),
        };

        query = sort.OrderByDescending switch
        {
            "Id" => query.OrderByDescending(u => u.Id),
            "CreatedAt" => query.OrderByDescending(u => u.CreatedAt),
            _ => query
        };

        return await query.ToListAsync(cancellationToken);
    }

}
