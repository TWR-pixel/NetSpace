using NetSpace.User.Domain.User;

namespace NetSpace.User.UseCases.User;

public interface IUserRepository : IRepository<UserEntity, string>
{
}
