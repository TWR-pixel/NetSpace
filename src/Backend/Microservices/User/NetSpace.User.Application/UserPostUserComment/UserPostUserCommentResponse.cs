using NetSpace.User.Application.Common;

namespace NetSpace.User.Application.UserPostUserComment;

public sealed record UserPostUserCommentResponse : ResponseBase
{
    public int Id { get; set; }
    public required string Body { get; set; }

    public Guid UserId { get; set; }
    public int UserPostId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
