using NetSpace.User.Domain.User;

namespace NetSpace.User.Domain.UserPost;

public sealed class UserPostEntity : IEntity<int>
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }

    public Guid UserId { get; set; }
    public UserEntity User { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public UserPostEntity()
    {

    }
}
