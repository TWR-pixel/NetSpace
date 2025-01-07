using NetSpace.Community.Application.Common.MediatR;
using NetSpace.Community.Application.CommunityPost;
using NetSpace.Community.Application.User;

namespace NetSpace.Community.Application.Community;

public sealed record CommunityResponse : ResponseBase
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }

    public UserResponse? Owner { get; set; }
    public required Guid OwnerId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime LastNameUpdatedAt { get; set; }

    public IEnumerable<UserResponse> CommunitySubscribers { get; set; } = new List<UserResponse>();
    public IEnumerable<CommunityPostResponse> CommunityPosts { get; set; } = new List<CommunityPostResponse>();
}
