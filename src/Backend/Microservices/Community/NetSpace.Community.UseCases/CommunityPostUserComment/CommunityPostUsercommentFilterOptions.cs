namespace NetSpace.Community.UseCases.CommunityPostUserComment;

public sealed class CommunityPostUsercommentFilterOptions
{
    public int? Id { get; set; }
    public string? Body { get; set; }

    public Guid? OwnerId { get; set; }
    public bool IncludeOwner { get; set; } = false;

    public int? CommunityPostId { get; set; }
    public bool IncludeCommunityPost { get; set; } = false;
}
