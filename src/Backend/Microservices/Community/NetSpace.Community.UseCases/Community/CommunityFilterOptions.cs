namespace NetSpace.Community.UseCases.Community;

public sealed class CommunityFilterOptions
{
    public int Id { get; set; } = default;
    public string? Name { get; set; }
    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? LastNameUpdatedAt { get; set; }

    public Guid? OwnerId { get; set; }
    public bool IncludeOwner { get; set; } = false;
    public bool IncludeCommunitySubscribers { get; set; } = false;
    public bool IncludeCommunityPosts { get; set; } = false;
}
