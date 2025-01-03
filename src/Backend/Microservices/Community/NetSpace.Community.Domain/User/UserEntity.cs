﻿using NetSpace.Community.Domain.Community;
using NetSpace.Community.Domain.CommunityPostUserComment;

namespace NetSpace.Community.Domain.User;

public sealed class UserEntity : IEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
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
    public Language Language { get; set; } = Language.NotSet;
    public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.NotSet;
    public string CurrentCity { get; set; } = string.Empty;
    public string PersonalSite { get; set; } = string.Empty;

    public Gender Gender { get; set; } = Gender.NotSet;

    public string SchoolName { get; set; } = string.Empty;

    public IEnumerable<CommunityEntity> CreatedCommunities { get; set; } = new List<CommunityEntity>();
    public IEnumerable<CommunityEntity> CommunitySubscriptions { get; set; } = new List<CommunityEntity>();
    public IEnumerable<CommunityPostUserCommentEntity> CommunityPostUserComments { get; set; } = new List<CommunityPostUserCommentEntity>();
}