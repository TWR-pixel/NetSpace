﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetSpace.User.Domain.User;

namespace NetSpace.User.Infrastructure.UserPostUserComment;

public sealed class UserPostUserCommentEntityTypeConfiguration : IEntityTypeConfiguration<UserPostUserCommentEntity>
{
    public void Configure(EntityTypeBuilder<UserPostUserCommentEntity> builder)
    {
        builder
            .HasOne(b => b.Owner)
            .WithMany(b => b.UserPostUserComments)
            .HasForeignKey(b => b.UserId);

        builder
            .HasOne(b => b.UserPost)
            .WithMany(b => b.UserComments)
            .HasForeignKey(b => b.UserPostId);


    }
}
