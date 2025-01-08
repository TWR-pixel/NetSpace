using NetSpace.User.Domain.User;
using NetSpace.User.Domain.UserPost;

namespace NetSpace.User.Domain.UserPostUserComment;

public sealed class UserPostUserCommentEntity : IEntity<int>
{
    public int Id { get; set; }
    public required string Body { get; set; }

    public UserEntity Owner { get; set; }
    public Guid UserId { get; set; }

    public UserPostEntity UserPost { get; set; }
    public int UserPostId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public UserPostUserCommentEntity()
    {

    }
}
