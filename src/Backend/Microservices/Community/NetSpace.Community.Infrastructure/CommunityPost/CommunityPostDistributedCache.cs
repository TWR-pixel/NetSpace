using Microsoft.Extensions.Caching.Distributed;
using NetSpace.Community.Application.CommunityPost.Caching;
using NetSpace.Community.Domain.CommunityPost;
using NetSpace.Community.Infrastructure.Common;

namespace NetSpace.Community.Infrastructure.CommunityPost;

public sealed class CommunityPostDistributedCache(IDistributedCache cache) : DistributedCacheBase<CommunityPostEntity, int>(cache), ICommunityPostDistributedCache
{
}
