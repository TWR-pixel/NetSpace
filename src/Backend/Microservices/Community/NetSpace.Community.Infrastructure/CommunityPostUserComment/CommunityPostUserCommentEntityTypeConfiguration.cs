using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetSpace.Community.Domain.CommunityPostUserComment;

namespace NetSpace.Community.Infrastructure.CommunityPostUserComment;

public sealed class CommunityPostUserCommentEntityTypeConfiguration : IEntityTypeConfiguration<CommunityPostUserCommentEntity>
{
    public void Configure(EntityTypeBuilder<CommunityPostUserCommentEntity> builder)
    {
        builder.Property(b => b.Body)
            .IsRequired(true)
            .HasMaxLength(1024);
    }
}
