using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Application.User;

public sealed record UserResponse : ResponseBase
{
    public required string Nickname { get; set; }
    public required string UserName { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;

    public string Hometown { get; set; } = string.Empty;
    public Language Language { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string CurrentCity { get; set; } = string.Empty;
    public string PersonalSite { get; set; } = string.Empty;

    public Gender Gender { get; set; }

    public string SchoolName { get; set; } = string.Empty;
}