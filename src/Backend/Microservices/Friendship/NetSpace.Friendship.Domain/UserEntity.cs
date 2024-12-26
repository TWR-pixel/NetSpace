using NetSpace.Common.Domain;
using System.Diagnostics.CodeAnalysis;

namespace NetSpace.Friendship.Domain;

public sealed class UserEntity : IEntity<string>
{
    public string Id { get; set; }
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

    public UserEntity()
    {
        
    }

    [SetsRequiredMembers]
    public UserEntity(string id, string nickname, string name, string surname, string lastName, string about, string avatarUrl, DateTime? birthDate, DateTime registrationDate, DateTime lastLoginAt, Gender gender)
    {
        Id = id;
        Nickname = nickname;
        Name = name;
        Surname = surname;
        LastName = lastName;
        About = about;
        AvatarUrl = avatarUrl;
        BirthDate = birthDate;
        RegistrationDate = registrationDate;
        LastLoginAt = lastLoginAt;
        Gender = gender;
    }

}
