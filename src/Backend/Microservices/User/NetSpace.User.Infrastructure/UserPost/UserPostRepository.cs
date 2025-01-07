using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NetSpace.User.Domain.UserPost;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.Infrastructure.UserPost;

public sealed class UserPostRepository(NetSpaceDbContext dbContext) : RepositoryBase<UserPostEntity, int>(dbContext), IUserPostRepository, IUserPostReadonlyRepository
{
    public async Task<IEnumerable<UserPostEntity>> FilterAsync(UserPostFilterOptions filter,
                                                               PaginationOptions pagination,
                                                               SortOptions sort,
                                                               CancellationToken cancellationToken = default)
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

        if (filter.IncludeUser)
            query = query.Include(u => u.User);

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

        query = query
            .Skip((pagination.PageCount - 1) * pagination.PageSize)
            .Take(pagination.PageSize);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<UserPostEntity?> GetByIdWithDetails(int id, CancellationToken cancellationToken = default)
    {
        var result = await DbContext.UserPosts
            .Include(u => u.User)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        return result;
    }

    public async Task<IEnumerable<UserPostEntity>> GetLatests(PaginationOptions pagination, CancellationToken cancellationToken = default)
    {
        var result = await DbContext.UserPosts
            .OrderBy(u => u.CreatedAt)
            .Skip((pagination.PageCount - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToArrayAsync(cancellationToken);

        return result;
    }
}
