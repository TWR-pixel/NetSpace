using System.Diagnostics.CodeAnalysis;

namespace NetSpace.Common.Messages.User;

[method: SetsRequiredMembers]
public sealed class UserDeletedMessage(Guid id, string name, string surname, DateTime? birthDate)
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
}
