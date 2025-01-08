namespace NetSpace.User.UseCases.UserPostUserComment;

public sealed class UserPostUserCommentFilterOptions
{
    public int? Id { get; set; }
    public string? Body { get; set; }

    public Guid? OwnerId { get; set; }
    public bool IncludeOwner { get; set; } = false;

    public int? UserPostId { get; set; }
    public bool IncludeUserPost { get; set; } = false;

    public DateTime? CreatedAt { get; set; }
}
