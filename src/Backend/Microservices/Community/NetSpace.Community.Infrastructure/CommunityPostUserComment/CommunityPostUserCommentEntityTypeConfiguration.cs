using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetSpace.Community.Domain.CommunityPostUserComment;

namespace NetSpace.Community.Infrastructure.CommunityPostUserComment;

public sealed class CommunityPostUserCommentEntityTypeConfiguration : IEntityTypeConfiguration<CommunityPostUserCommentEntity>
{
    public void Configure(EntityTypeBuilder<CommunityPostUserCommentEntity> builder)
    {
        builder
            .HasOne(b => b.Owner)
            .WithMany(b => b.CommunityPostUserComments)
            .HasForeignKey(b => b.OwnerId);

        builder
            .HasOne(b => b.CommunityPost)
            .WithMany(b => b.UserComments)
            .HasForeignKey(b => b.CommunityPostId);
    }
}
