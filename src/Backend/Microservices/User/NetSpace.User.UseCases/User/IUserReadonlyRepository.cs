using NetSpace.User.Domain.User;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.Common.Repositories;

namespace NetSpace.User.UseCases.User;

public interface IUserReadonlyRepository : IReadonlyRepository<UserEntity, Guid>
{
    public Task<IEnumerable<UserEntity>> FilterAsync(UserFilterOptions filter,
                                                 PaginationOptions pagination,
                                                 SortOptions sort,
                                                 CancellationToken cancellationToken = default);
}
