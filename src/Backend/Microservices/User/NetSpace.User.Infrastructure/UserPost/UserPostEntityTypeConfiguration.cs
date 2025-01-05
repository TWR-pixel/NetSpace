using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetSpace.User.Domain.UserPost;

namespace NetSpace.User.Infrastructure.UserPost;

public sealed class UserPostEntityTypeConfiguration : IEntityTypeConfiguration<UserPostEntity>
{
    public void Configure(EntityTypeBuilder<UserPostEntity> builder)
    {
        builder.Property(b => b.Title)
            .IsRequired(true)
            .HasMaxLength(256);

        builder.Property(b => b.Body)
            .IsRequired(true)
            .HasMaxLength(2048);

        builder
            .HasOne(b => b.User)
            .WithMany(b => b.UserPosts)
            .HasForeignKey(b => b.UserId);

        builder.HasMany(b => b.UserComments)
            .WithOne(b => b.UserPost)
            .HasForeignKey(b => b.UserPostId);
    }
}
