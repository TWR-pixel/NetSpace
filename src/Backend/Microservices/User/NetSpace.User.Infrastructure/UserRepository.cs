using NetSpace.Common.Infrastructure.EntityFrameworkCore;
using NetSpace.User.Domain;
using NetSpace.User.Infrastructure.Common;
using NetSpace.User.UseCases;

namespace NetSpace.User.Infrastructure;

public sealed class UserRepository(NetSpaceDbContext dbContext) : RepositoryBase<UserEntity, string, NetSpaceDbContext>(dbContext), IUserRepository
{
}
