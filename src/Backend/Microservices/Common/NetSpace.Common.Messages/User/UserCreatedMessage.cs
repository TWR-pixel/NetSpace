﻿using System.Diagnostics.CodeAnalysis;

namespace NetSpace.Common.Messages.User;


public sealed class UserCreatedMessage
{
    [SetsRequiredMembers]
    public UserCreatedMessage(string id, string nickname, string name, string surname, string lastName, string about, string avatarUrl)
    {
        Id = id;
        Nickname = nickname;
        Name = name;
        Surname = surname;
        LastName = lastName;
        About = about;
        AvatarUrl = avatarUrl;
    }

    public string Id { get; set; }
    public required string Nickname { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string LastName { get; set; } 
    public string About { get; set; }
    public string AvatarUrl { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;
}
