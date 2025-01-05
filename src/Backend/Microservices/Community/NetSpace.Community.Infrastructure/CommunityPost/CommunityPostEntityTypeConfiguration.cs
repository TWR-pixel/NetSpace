using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetSpace.Community.Domain.CommunityPost;

namespace NetSpace.Community.Infrastructure.CommunityPost;

public sealed class CommunityPostEntityTypeConfiguration : IEntityTypeConfiguration<CommunityPostEntity>
{
    public void Configure(EntityTypeBuilder<CommunityPostEntity> builder)
    {
        builder.Property(b => b.Title)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(b => b.Body)
            .IsRequired(true)
            .HasMaxLength(1024);

        builder.HasOne(b => b.Community)
            .WithMany(b => b.CommunityPosts)
            .HasForeignKey(b => b.CommunityId);

        builder.HasMany(b => b.UserComments)
            .WithOne(b => b.CommunityPost)
            .HasForeignKey(b => b.CommunityPostId);
    }
}
