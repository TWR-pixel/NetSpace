using NetSpace.Community.Domain;

namespace NetSpace.Community.UseCases.User;

public interface IUserRepository : IRepository<UserEntity, string>
{
}
