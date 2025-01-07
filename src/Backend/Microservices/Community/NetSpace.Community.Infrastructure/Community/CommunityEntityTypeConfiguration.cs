using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetSpace.Community.Domain.Community;

namespace NetSpace.Community.Infrastructure.Community;

public sealed class CommunityEntityTypeConfiguration : IEntityTypeConfiguration<CommunityEntity>
{
    public void Configure(EntityTypeBuilder<CommunityEntity> builder)
    {

        builder.Property(b => b.Name)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(b => b.Description)
            .IsRequired(false)
            .HasMaxLength(512);

        builder.Property(b => b.AvatarUrl)
            .IsRequired(false);

        builder.Property(b => b.CreatedAt)
            .IsRequired(true);

        builder.Property(b => b.LastNameUpdatedAt)
            .IsRequired(true);
    }
}
