using NetSpace.User.Domain.UserPostUserComment;

namespace NetSpace.User.Domain.User;

public sealed class UserPostEntity : IEntity<int>
{
    public int Id { get; set; }
    public required string Title { get; set; } 
    public required string Body { get; set; }

    public Guid UserId { get; set; }
    public UserEntity User { get; set; }

    public IEnumerable<UserPostUserCommentEntity> UserComments { get; set; } = new List<UserPostUserCommentEntity>();

    public UserPostEntity()
    {
        
    }
}
