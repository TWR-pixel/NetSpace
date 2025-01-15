using NetSpace.Common.Injector;
using NetSpace.Community.Domain.User;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Infrastructure.User;

[Inject(ImplementationsFor = [typeof(IUserRepository), typeof(IUserReadonlyRepository)])]
public sealed class UserRepository(NetSpaceDbContext dbContext) : RepositoryBase<UserEntity, Guid>(dbContext), IUserRepository
{
}
