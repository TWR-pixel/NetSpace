using NetSpace.Common.UseCases;
using NetSpace.UserPosts.Domain;

namespace NetSpace.User.UseCases;

public interface IUserPostRepository : IRepository<UserPostEntity, int>
{
}
