using Microsoft.Extensions.Caching.Memory;
using NetSpace.User.Application.UserPostUserComment;
using NetSpace.User.Domain.UserPostUserComment;
using NetSpace.User.Tests.Unit.Initializer.Caching;

namespace NetSpace.User.Tests.Unit.Application.UserPostUserComment;

public sealed class FakeUserPostUserCommentDistributedCacheStorage(IMemoryCache cache) : InMemoryDistributedCacheStorageBase<UserPostUserCommentEntity, int>(cache), IUserPostUsercommentDistrubutedCacheStorage
{

}
