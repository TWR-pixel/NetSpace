using Microsoft.EntityFrameworkCore;
using NetSpace.User.Domain.UserPostUserComment;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.Infrastructure.UserPostUserComment;

public sealed class UserPostUserCommentRepository(NetSpaceDbContext dbContext) 
    : RepositoryBase<UserPostUserCommentEntity, int>(dbContext), IUserPostUserCommentRepository, IUserPostUserCommentReadonlyRepository
{
    public async Task<IEnumerable<UserPostUserCommentEntity>> FilterAsync(UserPostUserCommentFilterOptions filter,
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
                .Where(u => u.UserId == filter.OwnerId);

        if (filter.UserPostId != null)
            query = query
                .Where(u => u.UserPostId == filter.UserPostId);

        query = sort.OrderByAscending switch
        {
            "Id" => query.OrderBy(u => u.Id),
            "CreatedAt" => query.OrderBy(u => u.CreatedAt),
            "Body" => query.OrderBy(u => u.Body),
            _ => query.OrderBy(u => u.Id)
        };

        query = sort.OrderByDescending switch
        {
            "Id" => query.OrderByDescending(u => u.Id),
            "CreatedAt" => query.OrderByDescending(u => u.CreatedAt),
            "Body" => query.OrderByDescending(u => u.Body),
            _ => query.OrderBy(u => u.Id)
        };

        query = query
            .Skip((pagination.PageCount - 1) * pagination.PageSize)
            .Take(pagination.PageSize);


        return await query.ToListAsync(cancellationToken);
    }

}
