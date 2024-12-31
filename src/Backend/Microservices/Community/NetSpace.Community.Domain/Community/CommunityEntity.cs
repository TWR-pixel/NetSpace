using NetSpace.Community.Domain.User;

namespace NetSpace.Community.Domain.Community;

public sealed class CommunityEntity : IEntity<int>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }

    public required UserEntity Owner { get; set; }
    public required string OwnerId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastNameUpdatedAt { get; set; } = DateTime.UtcNow;


}
