namespace NetSpace.Community.Domain.Community;

public sealed class CommunityPostEntity : IEntity<int>
{
    public int Id { get; set; }

    public required string Title { get; set; }
    public required string Body { get; set; }

    public required CommunityEntity Community { get; set; }
    public int CommunityId { get; set; }

    public CommunityPostEntity()
    {
        
    }
}
