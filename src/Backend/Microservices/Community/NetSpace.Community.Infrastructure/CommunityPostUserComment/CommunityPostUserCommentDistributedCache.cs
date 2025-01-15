using Microsoft.Extensions.Caching.Distributed;
using NetSpace.Common.Injector;
using NetSpace.Community.Application.CommunityPostUserComment.Caching;
using NetSpace.Community.Domain.CommunityPostUserComment;
using NetSpace.Community.Infrastructure.Common;

namespace NetSpace.Community.Infrastructure.CommunityPostUserComment;

[Inject(ImplementationsFor = [typeof(ICommunityPostUserCommentDistributedCache)])]
public sealed class CommunityPostUserCommentDistributedCache(IDistributedCache cache)
    : DistributedCacheBase<CommunityPostUserCommentEntity, int>(cache), ICommunityPostUserCommentDistributedCache
{
}
