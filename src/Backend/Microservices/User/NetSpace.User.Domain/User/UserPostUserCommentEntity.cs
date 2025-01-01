﻿namespace NetSpace.User.Domain.User;

public sealed class UserPostUserCommentEntity : IEntity<int>
{
    public int Id { get; set; }
    public required string Body { get; set; }
    
    public UserEntity? Owner { get; set; }
    public required Guid UserId { get; set; }

    public UserPostEntity? UserPost { get; set; }
    public required int UserPostId { get;set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public UserPostUserCommentEntity()
    {
        
    }
}
