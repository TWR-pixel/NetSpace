using NetSpace.User.Domain.User;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.Common.Repositories;

namespace NetSpace.User.UseCases.User;

public interface IUserRepository : IRepository<UserEntity, Guid>
{

}
