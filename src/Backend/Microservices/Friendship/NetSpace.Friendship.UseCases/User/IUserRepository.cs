using NetSpace.Friendship.Domain.User;

namespace NetSpace.Friendship.UseCases.User;

public interface IUserRepository : IRepository<UserEntity, Guid>
{

}
