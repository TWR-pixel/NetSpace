namespace NetSpace.Identity.Infrastructure.User;

public sealed class UserRepository(NetSpaceDbContext dbContext) : RepositoryBase<UserEntity, string, NetSpaceDbContext>(dbContext), IUserRepository
{
}
