﻿using NetSpace.User.Domain.User;
using System.Diagnostics.CodeAnalysis;

namespace NetSpace.User.Application.User;

[method: SetsRequiredMembers]
public sealed record UserResponse(string Id,
                                  string Nickname,
                                  string Name,
                                  string Surname,
                                  string LastName = "",
                                  string About = "",
                                  string AvatarUrl = "",
                                  string Password = "",
                                  DateTime? BirthDate = null,
                                  Gender Gender = Gender.NotSet) : ResponseBase
{
    public string Id { get; set; } = Id;
    public required string Nickname { get; set; } = Nickname;
    public required string Name { get; set; } = Name;
    public required string Surname { get; set; } = Surname;
    public required string Password { get; set; } = Password;

    public string LastName { get; set; } = LastName;
    public string About { get; set; } = About;
    public string AvatarUrl { get; set; } = AvatarUrl;
    public DateTime? BirthDate { get; set; } = BirthDate;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;

    public Gender Gender { get; set; } = Gender;
}
