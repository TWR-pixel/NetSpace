using NetSpace.Common.Domain;
using System.Diagnostics.CodeAnalysis;

namespace NetSpace.Friendship.Domain;

[method: SetsRequiredMembers]
public sealed class UserEntity(Guid id, string name, string surname, DateTime? birthDate, Gender gender) : IEntity<Guid>
{
    public Guid Id { get; set; } = id;
    public required string Nickname { get; set; } = "";
    public required string Name { get; set; } = name;
    public required string Surname { get; set; } = surname;
    public string LastName { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; } = birthDate;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;

    public Gender Gender { get; set; } = gender;

    public IList<UserEntity> Followers { get; set; } = [];
}
