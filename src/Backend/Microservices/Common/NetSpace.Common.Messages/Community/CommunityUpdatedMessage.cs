namespace NetSpace.Common.Messages.Community;

public sealed record CommunityUpdatedMessage(string Name, string Description, string AvatarUrl, string OwnerId, DateTime LastNameUpdatedAt, CommunityType CommunityType)
{
    public required string Name { get; set; } = Name;
    public string? Description { get; set; } = Description;
    public string? AvatarUrl { get; set; } = AvatarUrl;

    public required string OwnerId { get; set; } = OwnerId;
    public required DateTime LastNameUpdatedAt { get; set; } = LastNameUpdatedAt;

    public CommunityType CommunityType { get; set; } = CommunityType;
}
