using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetSpace.User.Domain.UserPost;

namespace NetSpace.User.Infrastructure.UserPost;

public sealed class UserPostEntityTypeConfiguration : IEntityTypeConfiguration<UserPostEntity>
{
    public void Configure(EntityTypeBuilder<UserPostEntity> builder)
    {
        builder
            .HasOne(b => b.User)
            .WithMany(b => b.UserPosts)
            .HasForeignKey(b => b.UserId);
    }
}
