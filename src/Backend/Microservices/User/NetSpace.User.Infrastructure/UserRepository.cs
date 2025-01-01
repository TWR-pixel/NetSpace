using NetSpace.User.Domain.User;
using NetSpace.User.Infrastructure.Common;
using NetSpace.User.UseCases;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Infrastructure;

public sealed class UserRepository(NetSpaceDbContext dbContext) : RepositoryBase<UserEntity, Guid>(dbContext), IUserRepository
{
    public Task<IEnumerable<UserEntity>> FilterAsync(FilterOptions filter, PaginationOptions pagination, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
