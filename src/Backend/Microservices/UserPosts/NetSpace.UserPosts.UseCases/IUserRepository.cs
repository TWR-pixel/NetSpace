using NetSpace.Common.UseCases;
using NetSpace.UserPosts.Domain;

namespace NetSpace.UserPosts.UseCases;

public interface IUserRepository : IRepository<UserEntity, string>
{
}
