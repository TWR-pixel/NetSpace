namespace NetSpace.Community.UseCases.CommunityPost;

public sealed class CommunityPostFilterOptions
{
    public int? Id { get; set; }

    public string? Title { get; set; }
    public string? Body { get; set; }

    public int? CommunityId { get; set; }
    public bool IncludeCommunity { get; set; } = false;
}
