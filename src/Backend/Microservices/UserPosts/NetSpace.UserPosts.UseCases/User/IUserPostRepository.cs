using NetSpace.UserPosts.Domain.User;

namespace NetSpace.UserPosts.UseCases.User;

public interface IUserPostRepository : IRepository<UserPostEntity, int>
{
}
