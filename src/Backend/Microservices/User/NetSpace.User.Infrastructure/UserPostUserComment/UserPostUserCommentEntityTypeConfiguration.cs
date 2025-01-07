using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetSpace.User.Domain.UserPostUserComment;

namespace NetSpace.User.Infrastructure.UserPostUserComment;

public sealed class UserPostUserCommentEntityTypeConfiguration : IEntityTypeConfiguration<UserPostUserCommentEntity>
{
    public void Configure(EntityTypeBuilder<UserPostUserCommentEntity> builder)
    {
        builder.Property(b => b.Body)
            .IsRequired(true)
            .HasMaxLength(512);
    }
}
