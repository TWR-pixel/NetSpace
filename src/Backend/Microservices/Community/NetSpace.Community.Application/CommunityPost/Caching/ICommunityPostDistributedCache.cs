using NetSpace.Community.Application.Common.Caching;
using NetSpace.Community.Domain.CommunityPost;

namespace NetSpace.Community.Application.CommunityPost.Caching;

public interface ICommunityPostDistributedCache : IDistributedCache<CommunityPostEntity, int>
{
}
