using NetSpace.User.Application.UserPostUserComment;

namespace NetSpace.User.Application.UserPost;

public sealed record UserPostResponse : ResponseBase
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }

    public Guid UserId { get; set; }

    public IEnumerable<UserPostUserCommentResponse> UserComments { get; set; } = new List<UserPostUserCommentResponse>();
}
