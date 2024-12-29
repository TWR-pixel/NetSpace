using NetSpace.Common.UseCases;
using NetSpace.Community.Domain;

namespace NetSpace.Community.UseCases;

public interface IUserRepository : IRepository<UserEntity, string>
{
}
