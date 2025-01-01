using System.Diagnostics.CodeAnalysis;

namespace NetSpace.Common.Messages.User;

[method: SetsRequiredMembers]
public sealed class UserUpdatedMessage(Guid id,string name, string surname,string email, DateTime? birthDate, Gender gender = Gender.NotSet)
{
    public Guid Id { get; set; } = id;
    public required string Nickname { get; set; } = "";
    public required string Name { get; set; } = name;
    public required string Surname { get; set; } = surname;
    public required string Email { get; set; } = email;
    public string LastName { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; } = birthDate;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;

    public Gender Gender { get; set; } = gender;
}
