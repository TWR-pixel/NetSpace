using System.Diagnostics.CodeAnalysis;

namespace NetSpace.User.Domain.User;

[method: SetsRequiredMembers]
public sealed class UserPostEntity : IEntity<int>
{
    public int Id { get; set; }
    public required string Title { get; set; } 
    public required string Body { get; set; }
    public required UserEntity User { get; set; }

    public UserPostEntity()
    {
        
    }
}
