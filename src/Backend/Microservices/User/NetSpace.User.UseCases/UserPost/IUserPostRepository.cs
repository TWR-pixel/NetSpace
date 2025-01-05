using NetSpace.User.Domain.UserPost;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.Common.Repositories;

namespace NetSpace.User.UseCases.UserPost;

public interface IUserPostRepository : IRepository<UserPostEntity, int>
{
}
