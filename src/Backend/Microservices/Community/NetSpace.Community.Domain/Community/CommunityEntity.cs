using NetSpace.Community.Domain.CommunityPost;
using NetSpace.Community.Domain.User;

namespace NetSpace.Community.Domain.Community;

public sealed class CommunityEntity : IEntity<int>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }

    public UserEntity Owner { get; set; }
    public required Guid OwnerId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastNameUpdatedAt { get; set; } = DateTime.UtcNow;

    public IEnumerable<UserEntity> CommunitySubscribers { get; set; } = new List<UserEntity>();
    public IEnumerable<CommunityPostEntity> CommunityPosts { get; set; } = new List<CommunityPostEntity>();
}
