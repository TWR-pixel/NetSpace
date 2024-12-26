namespace NetSpace.Common.Messages.Community;

public sealed record CommunityUpdatedMessage(string name, string description, string avatarUrl, string ownerId, DateTime lastNameUpdatedAt, CommunityType communityType)
{
    public required string Name { get; set; } = name;
    public string? Description { get; set; } = description;
    public string? AvatarUrl { get; set; } = avatarUrl;
    
    public required string OwnerId { get; set; } = ownerId;
    public required DateTime LastNameUpdatedAt { get; set; } = DateTime.UtcNow;

    public CommunityType CommunityType { get; set; } = communityType;
}
