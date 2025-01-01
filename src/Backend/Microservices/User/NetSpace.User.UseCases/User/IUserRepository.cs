using NetSpace.User.Domain.User;

namespace NetSpace.User.UseCases.User;

public interface IUserRepository : IRepository<UserEntity, Guid>
{
    public Task<IEnumerable<UserEntity>> FilterAsync(FilterOptions filter, PaginationOptions pagination, CancellationToken cancellationToken = default);
}
