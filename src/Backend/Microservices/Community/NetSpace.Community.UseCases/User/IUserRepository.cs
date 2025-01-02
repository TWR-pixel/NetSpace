using NetSpace.Community.Domain.User;

namespace NetSpace.Community.UseCases.User;

public interface IUserRepository : IRepository<UserEntity, Guid>
{
}
