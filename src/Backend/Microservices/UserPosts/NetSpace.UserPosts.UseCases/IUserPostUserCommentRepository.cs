using NetSpace.Common.UseCases;
using NetSpace.UserPosts.Domain;

namespace NetSpace.UserPosts.UseCases;

public interface IUserPostUserCommentRepository : IRepository<UserPostUserCommentEntity, int>
{
}
