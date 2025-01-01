using NetSpace.Friendship.Domain;

namespace NetSpace.Friendship.UseCases.User;

public interface IUserRepository : IRepository<UserEntity, Guid>
{

}
