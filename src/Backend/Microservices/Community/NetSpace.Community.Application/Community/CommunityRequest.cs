namespace NetSpace.Community.Application.Community;

public sealed record CommunityRequest : RequestBase<CommunityResponse>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }

    public required Guid OwnerId { get; set; }
}
