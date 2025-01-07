using NetSpace.User.Application.Common.Cache;
using NetSpace.User.Domain.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment;

public interface IUserPostUsercommentDistrubutedCacheStorage : IDistributedCacheStorage<UserPostUserCommentEntity, int>
{
}
