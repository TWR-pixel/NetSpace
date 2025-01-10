using Microsoft.Extensions.Caching.Distributed;
using NetSpace.User.Application.UserPostUserComment;
using NetSpace.User.Domain.UserPostUserComment;
using NetSpace.User.Infrastructure.Common.Cache;

namespace NetSpace.User.Infrastructure.UserPostUserComment;

public sealed class UserPostUserCommentDistrubutedCacheStorage(IDistributedCache cache) : DistributedCacheStorageBase<UserPostUserCommentEntity, int>(cache), IUserPostUsercommentDistrubutedCacheStorage
{
}
