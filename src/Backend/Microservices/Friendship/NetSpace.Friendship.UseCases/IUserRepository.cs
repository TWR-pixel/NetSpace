using NetSpace.Common.UseCases;
using NetSpace.Friendship.Domain;

namespace NetSpace.Friendship.UseCases;

public interface IUserRepository : IRepository<UserEntity, Guid>
{
}
