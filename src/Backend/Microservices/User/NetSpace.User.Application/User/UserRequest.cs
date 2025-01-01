using NetSpace.User.Domain.User;

namespace NetSpace.User.Application.User;

public sealed record UserRequest : RequestBase<UserResponse>
{
    public required string Nickname { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; } 
    public required string Email { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }

    public string Hometown { get; set; } = string.Empty;
    public Language Language { get; set; } = Language.NotSet;
    public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.NotSet;
    public string CurrentCity { get; set; } = string.Empty;
    public string PersonalSite { get; set; } = string.Empty;

    public Gender Gender { get; set; } = Gender.NotSet;

    public string SchoolName { get; set; } = string.Empty;
}
