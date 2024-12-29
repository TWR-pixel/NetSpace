using NetSpace.Common.Infrastructure.EntityFrameworkCore;
using NetSpace.Community.Domain;
using NetSpace.Community.UseCases;

namespace NetSpace.Community.Infrastructure;

public sealed class UserRepository(NetSpaceDbContext dbContext) : RepositoryBase<UserEntity, string, NetSpaceDbContext>(dbContext), IUserRepository
{
}
