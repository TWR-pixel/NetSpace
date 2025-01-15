using NetSpace.User.Domain.UserPost;
using NetSpace.User.UseCases.Common.Repositories;

namespace NetSpace.User.UseCases.UserPost;

public interface IUserPostRepository : IRepository<UserPostEntity, int>
{
    public Task<IEnumerable<UserPostEntity>> GetAllByUserId(Guid userId, CancellationToken cancellationToken = default);
}
