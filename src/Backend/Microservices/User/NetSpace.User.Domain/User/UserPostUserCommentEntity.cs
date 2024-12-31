namespace NetSpace.User.Domain.User;

public sealed class UserPostUserCommentEntity(string body, UserEntity owner, UserPostEntity userPost) : IEntity<int>
{
    public int Id { get; set; }

    public required string Body { get; set; } = body;
    public required UserEntity Owner { get; set; } = owner;
    public required UserPostEntity UserPost { get; set; } = userPost;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
