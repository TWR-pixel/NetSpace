using NetSpace.User.Domain.User;
using NetSpace.User.UseCases;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.Infrastructure.UserPostUserComment;

public sealed class UserPostUserCommentRepository(NetSpaceDbContext dbContext) : RepositoryBase<UserPostUserCommentEntity, int>(dbContext), IUserPostUserCommentRepository
{
    public IQueryable<UserPostUserCommentEntity> Filter(UserPostUserCommentFilterOptions filter, PaginationOptions pagination, SortOptions sort)
    {
        var query = DbContext.UserPostUserComments.AsQueryable();

        if (filter.Id != null)
            query = query.Where(u => u.Id == filter.Id);

        if (filter.CreatedAt != null)
            query = query.Where(u => u.CreatedAt == filter.CreatedAt);

        if (!string.IsNullOrWhiteSpace(filter.Body))
            query = query.Where(u => u.Body == filter.Body);

        return query;
    }

}
