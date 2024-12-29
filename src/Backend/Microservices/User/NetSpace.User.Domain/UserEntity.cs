﻿using Microsoft.AspNetCore.Identity;
using NetSpace.Common.Domain;
using System.Diagnostics.CodeAnalysis;

namespace NetSpace.User.Domain;

[method: SetsRequiredMembers]
public sealed class UserEntity(string nickname,
                               string name,
                               string surname,
                               string lastName = "",
                               string about = "",
                               string avatarUrl = "",
                               DateTime? birthDate = null,
                               Gender gender = Gender.NotSet) : IdentityUser, IEntity<string>
{
    public required string Nickname { get; set; } = nickname;
    public required string Name { get; set; } = name;
    public required string Surname { get; set; } = surname;
    public string LastName { get; set; } = lastName;
    public string About { get; set; } = about;
    public string AvatarUrl { get; set; } = avatarUrl;
    public DateTime? BirthDate { get; set; } = birthDate;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;

    public string Hometown { get; set; } = string.Empty;
    public Language Language { get; set; } = Language.NotSet;
    public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.NotSet;
    public string CurrentCity { get; set; } = string.Empty;
    public string PersonalSite { get; set; } = string.Empty;

    public Gender Gender { get; set; } = gender;

    public string SchoolName { get; set; } = string.Empty;
}