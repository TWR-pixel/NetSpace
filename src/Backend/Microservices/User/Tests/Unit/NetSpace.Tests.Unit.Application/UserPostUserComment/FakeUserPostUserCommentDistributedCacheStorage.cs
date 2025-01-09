using Microsoft.Extensions.Caching.Memory;
using NetSpace.Tests.Unit.Initializer.Caching;
using NetSpace.User.Application.UserPostUserComment;
using NetSpace.User.Domain.UserPostUserComment;

namespace NetSpace.Tests.Unit.Application.UserPostUserComment;

public sealed class FakeUserPostUserCommentDistributedCacheStorage(IMemoryCache cache) : InMemoryDistributedCacheStorageBase<UserPostUserCommentEntity, int>(cache), IUserPostUsercommentDistrubutedCacheStorage
{

}
