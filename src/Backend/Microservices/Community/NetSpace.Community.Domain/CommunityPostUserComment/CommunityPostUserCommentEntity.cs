using NetSpace.Community.Domain.CommunityPost;
using NetSpace.Community.Domain.User;

namespace NetSpace.Community.Domain.CommunityPostUserComment;

public sealed class CommunityPostUserCommentEntity : IEntity<int>
{
    public int Id { get; set; }
    public required string Body { get; set; }

    public UserEntity Owner { get; set; }
    public Guid OwnerId { get; set; }

    public CommunityPostEntity CommunityPost { get; set; }
    public int CommunityPostId { get; set; }

    public CommunityPostUserCommentEntity()
    {

    }
}
