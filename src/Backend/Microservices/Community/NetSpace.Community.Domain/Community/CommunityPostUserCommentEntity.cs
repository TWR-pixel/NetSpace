using NetSpace.Community.Domain.User;

namespace NetSpace.Community.Domain.Community;

public sealed class CommunityPostUserCommentEntity : IEntity<int>
{
    public int Id { get; set; }
    public required string Body { get; set; }

    public required UserEntity Owner { get; set; }
    public required string OwnerId { get; set; }

    public required CommunityPostEntity CommunityPost { get; set; }
    public int CommunityPostId { get; set; }

    public CommunityPostUserCommentEntity()
    {

    }
}
