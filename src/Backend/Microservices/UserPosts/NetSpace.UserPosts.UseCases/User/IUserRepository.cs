using NetSpace.UserPosts.Domain.User;

namespace NetSpace.UserPosts.UseCases.User;

public interface IUserRepository : IRepository<UserEntity, string>
{
}
