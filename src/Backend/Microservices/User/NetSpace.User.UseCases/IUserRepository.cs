using NetSpace.Common.UseCases;
using NetSpace.User.Domain;

namespace NetSpace.User.UseCases;

public interface IUserRepository : IRepository<UserEntity, string>
{
}
