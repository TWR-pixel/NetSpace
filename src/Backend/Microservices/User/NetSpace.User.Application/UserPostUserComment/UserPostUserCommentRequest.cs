namespace NetSpace.User.Application.UserPostUserComment;

public sealed record UserPostUserCommentRequest : RequestBase<UserPostUserCommentResponse>
{
    public required string Body { get; set; }

    public Guid UserId { get; set; }
    public int UserPostId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
