﻿namespace NetSpace.CommunitySubscribers.Domain;

public sealed class UserEntity(string id,
                               string nickname,
                               string name,
                               string surname,
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
    public string LastName { get; set; } = lastName;
    public string About { get; set; } = about;
    public string AvatarUrl { get; set; } = avatarUrl;
    public DateTime? BirthDate { get; set; } = birthDate;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;

    public Gender Gender { get; set; } = gender;
}
