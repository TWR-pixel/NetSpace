using NetSpace.User.Domain.User;

namespace NetSpace.User.UseCases.UserPost;

public interface IUserPostRepository : IRepository<UserPostEntity, int>
{
    public Task<IEnumerable<UserPostEntity>> FilterAsync(UserPostFilterOptions filter, PaginationOptions paginationk, SortOptions sort, CancellationToken cancellationToken = default);
}
