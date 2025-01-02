using NetSpace.User.Domain.User;

namespace NetSpace.User.UseCases.UserPost;

public interface IUserPostRepository : IRepository<UserPostEntity, int>
{
    public IQueryable<UserPostEntity> Filter(UserPostFilterOptions filter, PaginationOptions paginationk, SortOptions sort);
}
