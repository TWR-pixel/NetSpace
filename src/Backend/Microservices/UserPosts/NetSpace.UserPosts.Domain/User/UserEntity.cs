using System.Diagnostics.CodeAnalysis;

namespace NetSpace.UserPosts.Domain.User;

[method: SetsRequiredMembers]
public sealed class UserEntity(string id,
                               string nickname,
                               string name,
                               string surname,
                               string email,
                               string lastName = "",
                               string about = "",
                               string avatarUrl = "",
                               DateTime? birthDate = null,
                               Gender gender = Gender.NotSet) : IEntity<string>
{
    public string Id { get; set; } = id;
    public required string Nickname { get; set; } = nickname;
    public required string Name { get; set; } = name;
    public required string Surname { get; set; } = surname;
    public required string Email { get; set; } = email;
    public string LastName { get; set; } = lastName;
    public string About { get; set; } = about;
    public string AvatarUrl { get; set; } = avatarUrl;
    public DateTime? BirthDate { get; set; } = birthDate;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;

    public Gender Gender { get; set; } = gender;
}
