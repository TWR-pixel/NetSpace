using NetSpace.User.Domain.User;

namespace NetSpace.User.UseCases.User;

public interface IUserRepository : IRepository<UserEntity, Guid>
{
    public Task<IEnumerable<UserEntity>> FilterAsync(UserFilterOptions filter,
                                                     PaginationOptions pagination,
                                                     SortOptions sort,
                                                     CancellationToken cancellationToken = default);
}
