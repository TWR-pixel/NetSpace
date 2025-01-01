﻿namespace NetSpace.User.UseCases.UserPostUserComment;

public sealed class UserPostUserCommentFilterOptions
{
    public int? Id { get; set; }
    public string? Body { get; set; }
    public Guid? OwnerId { get; set; }
    public int? UserPostId { get; set; }
    public DateTime? CreatedAt { get; set; }
}
