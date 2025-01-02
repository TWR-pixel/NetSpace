using Microsoft.EntityFrameworkCore;
using NetSpace.User.Domain.User;
using NetSpace.User.UseCases;
using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.Infrastructure.UserPost;

public sealed class UserPostRepository(NetSpaceDbContext dbContext) : RepositoryBase<UserPostEntity, int>(dbContext), IUserPostRepository
{
    public async Task<IEnumerable<UserPostEntity>> FilterAsync(UserPostFilterOptions filter, PaginationOptions pagination, SortOptions sort, CancellationToken cancellationToken = default)
    {
        var query = DbContext.UserPosts.AsQueryable();

        if (filter.Id != null)
            query = query.Where(u => u.Id == filter.Id);

        if (!string.IsNullOrWhiteSpace(filter.Title))
            query = query.Where(u => u.Title == filter.Title);

        if (!string.IsNullOrWhiteSpace(filter.Body))
            query = query.Where(u => u.Body == filter.Body);

        if (filter.UserId != null)
            query = query
                .Where(u => u.UserId == filter.UserId);

        if (filter.IncludeUser == true)
        {
            query = query
                .Include(u => u.User);
        }

        query = query
            .Skip((pagination.PageCount - 1) * pagination.PageSize)
            .Take(pagination.PageSize);

        query = sort.OrderByAscending switch
        {
            "Id" => query.OrderBy(u => u.Id),
            "Title" => query.OrderBy(u => u.Title),
            "Body" => query.OrderBy(u => u.Body),
            "UserId" => query.OrderBy(u => u.UserId),
            _ => query
        };

        query = sort.OrderByDescending switch
        {
            "Id" => query.OrderByDescending(u => u.Id),
            "Title" => query.OrderByDescending(u => u.Title),
            "Body" => query.OrderByDescending(u => u.Body),
            "UserId" => query.OrderByDescending(u => u.UserId),
            _ => query
        };

        return await query.ToListAsync(cancellationToken);
    }
}
