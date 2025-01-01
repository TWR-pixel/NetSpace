namespace NetSpace.User.Domain.User;

public sealed class UserPostUserCommentEntity : IEntity<int>
{
    public int Id { get; set; }
    public required string Body { get; set; }
    public required UserEntity Owner { get; set; }
    public required UserPostEntity UserPost { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public UserPostUserCommentEntity()
    {
        
    }
}
