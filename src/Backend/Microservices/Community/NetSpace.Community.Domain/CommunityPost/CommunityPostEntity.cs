using NetSpace.Community.Domain.Community;

namespace NetSpace.Community.Domain.CommunityPost;

public sealed class CommunityPostEntity : IEntity<int>
{
    public int Id { get; set; }

    public required string Title { get; set; }
    public required string Body { get; set; }

    public CommunityEntity Community { get; set; }
    public int CommunityId { get; set; }

    public uint Likes { get; set; }
    public uint Dislikes { get; set; }

    public CommunityPostEntity()
    {

    }
}
