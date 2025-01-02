namespace NetSpace.Common.Messages.Community;

public sealed record CommunityUpdatedMessag
{
    public required string Name { get; set; } 
    public string? Description { get; set; } 
    public string? AvatarUrl { get; set; } 

    public required string OwnerId { get; set; } 
    public required DateTime LastNameUpdatedAt { get; set; } 

    public CommunityType CommunityType { get; set; }
}
