using Microsoft.Extensions.Caching.Distributed;
using NetSpace.Common.Injector;
using NetSpace.User.Application.UserPostUserComment;
using NetSpace.User.Domain.UserPostUserComment;
using NetSpace.User.Infrastructure.Common.Cache;

namespace NetSpace.User.Infrastructure.UserPostUserComment;


[Inject(ImplementationsFor = [typeof(IUserPostUsercommentDistrubutedCacheStorage)])]
public sealed class UserPostUserCommentDistrubutedCacheStorage(IDistributedCache cache) : DistributedCacheStorageBase<UserPostUserCommentEntity, int>(cache), IUserPostUsercommentDistrubutedCacheStorage
{
}
