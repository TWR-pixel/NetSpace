using NetSpace.Common.Domain;
using System.Diagnostics.CodeAnalysis;

namespace NetSpace.Friendship.Domain;

public sealed class UserEntity : IEntity<Guid>
{
    [SetsRequiredMembers]
    public UserEntity(Guid id, string name, string surname, DateTime? birthDate, Gender gender)
    {
        Id = id;
        Name = name;
        Surname = surname;
        BirthDate = birthDate;
        Gender = gender;
    }

    public UserEntity()
    {
        
    }

    public Guid Id { get; set; }
    public required string Nickname { get; set; } = "";
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;

    public Gender Gender { get; set; }

    public IList<UserEntity> Followers { get; set; } = [];
}
