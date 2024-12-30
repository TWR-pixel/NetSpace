namespace NetSpace.CommunitySubscribers.Domain;

public sealed class CommunityEntity(string name, string? description, string? avatarUrl, UserEntity owner, CommunityType communityType) : IEntity<int>
{
    public int Id { get; set; }
    public required string Name { get; set; } = name;
    public string? Description { get; set; } = description;
    public string? AvatarUrl { get; set; } = avatarUrl;

    public required UserEntity Owner { get; set; } = owner;

    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public required DateTime LastNameUpdatedAt { get; set; } = DateTime.UtcNow;

    public CommunityType CommunityType { get; set; } = communityType;
}
