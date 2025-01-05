using NetSpace.User.Domain.UserPost;
using NetSpace.User.UseCases.Common.Repositories;

namespace NetSpace.User.UseCases.UserPost;

public interface IUserPostReadonlyRepository : IReadonlyRepository<UserPostEntity, int>
{
}
