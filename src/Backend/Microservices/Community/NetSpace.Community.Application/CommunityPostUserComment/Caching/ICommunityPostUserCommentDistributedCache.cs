using NetSpace.Community.Application.Common.Caching;
using NetSpace.Community.Domain.CommunityPostUserComment;

namespace NetSpace.Community.Application.CommunityPostUserComment.Caching;

public interface ICommunityPostUserCommentDistributedCache : IDistributedCacheStorage<CommunityPostUserCommentEntity, int>
{

}
