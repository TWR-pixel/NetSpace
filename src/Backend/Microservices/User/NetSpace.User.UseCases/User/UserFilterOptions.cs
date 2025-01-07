using NetSpace.User.Domain.User;

namespace NetSpace.User.UseCases.User;

public sealed class UserFilterOptions
{
    public Guid? Id { get; set; }
    public string? Nickname { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? LastName { get; set; }
    public string? About { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public DateTime? LastLoginAt { get; set; }

    public string? Hometown { get; set; }
    public Language? Language { get; set; }
    public MaritalStatus? MaritalStatus { get; set; }
    public string? CurrentCity { get; set; }
    public string? PersonalSite { get; set; }

    public Gender? Gender { get; set; }

    public string? SchoolName { get; set; }
}
