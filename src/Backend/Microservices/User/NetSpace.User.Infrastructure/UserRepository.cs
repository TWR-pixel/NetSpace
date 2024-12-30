using NetSpace.User.Domain.User;
using NetSpace.User.Infrastructure.Common;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Infrastructure;

public sealed class UserRepository(NetSpaceDbContext dbContext) : RepositoryBase<UserEntity, string, NetSpaceDbContext>(dbContext), IUserRepository
{
}
