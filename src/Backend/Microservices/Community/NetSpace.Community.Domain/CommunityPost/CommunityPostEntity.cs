using NetSpace.Community.Domain.Community;
using NetSpace.Community.Domain.CommunityPostUserComment;

namespace NetSpace.Community.Domain.CommunityPost;

public sealed class CommunityPostEntity : IEntity<int>
{
    public int Id { get; set; }

    public required string Title { get; set; }
    public required string Body { get; set; }

    public CommunityEntity Community { get; set; }
    public int CommunityId { get; set; }

    public IEnumerable<CommunityPostUserCommentEntity> UserComments { get; set; } = new List<CommunityPostUserCommentEntity>();

    public CommunityPostEntity()
    {

    }
}
