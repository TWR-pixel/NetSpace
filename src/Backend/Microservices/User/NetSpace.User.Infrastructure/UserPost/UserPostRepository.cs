using NetSpace.User.Domain.User;
using NetSpace.User.UseCases;
using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.Infrastructure.UserPost;

public sealed class UserPostRepository(NetSpaceDbContext dbContext) : RepositoryBase<UserPostEntity, int>(dbContext), IUserPostRepository
{
    public IQueryable<UserPostEntity> Filter(UserPostFilterOptions filter, PaginationOptions paginationk, SortOptions sort)
    {
        throw new NotImplementedException();
    }
}
