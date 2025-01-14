using Microsoft.Extensions.Caching.Memory;
using NetSpace.Community.Application.Community.Caching;
using NetSpace.Community.Domain.Community;

namespace NetSpace.Community.Tests.Unit.Application.Community;

public sealed class CommunityDistributedCache(IMemoryCache cache) 
    : FakeInMemoryDistributedCache<CommunityEntity, int>(cache), ICommunityDistributedCache
{
}
