using NetSpace.Community.Domain.User;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Infrastructure;

public sealed class UserRepository(NetSpaceDbContext dbContext) : RepositoryBase<UserEntity, Guid>(dbContext), IUserRepository
{
}
