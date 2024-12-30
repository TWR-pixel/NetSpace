using System.Diagnostics.CodeAnalysis;

namespace NetSpace.UserPosts.Domain.User;

[method: SetsRequiredMembers]
public sealed class UserPostEntity(string title, string body, UserEntity user) : IEntity<int>
{
    public int Id { get; set; }

    public required string Title { get; set; } = title;
    public required string Body { get; set; } = body;
    public required UserEntity User { get; set; } = user;
}
