namespace NetSpace.User.Domain.User;

[method: SetsRequiredMembers]
public sealed class UserPostEntity : IEntity<int>
{
    public int Id { get; set; }
    public required string Title { get; set; } 
    public required string Body { get; set; }

    public Guid UserId { get; set; }
    public UserEntity User { get; set; }

    public IEnumerable<UserPostUserCommentEntity> UserComments { get; set; } = [];

    public UserPostEntity()
    {
        
    }
}
