using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetSpace.Community.Domain.Community;

namespace NetSpace.Community.Infrastructure.Community;

public sealed class CommunityEntityTypeConfiguration : IEntityTypeConfiguration<CommunityEntity>
{
    public void Configure(EntityTypeBuilder<CommunityEntity> builder)
    {
        builder.HasOne(b => b.Owner)
            .WithMany(b => b.CreatedCommunities)
            .HasForeignKey(b => b.OwnerId);

        builder.HasMany(b => b.CommunitySubscribers)
            .WithMany(b => b.CommunitySubscriptions);

        builder.Property(b => b.Name)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(b => b.Description)
            .IsRequired(false)
            .HasMaxLength(512);

        builder.Property(b => b.AvatarUrl)
            .IsRequired(false);

        builder.HasOne(b => b.Owner)
            .WithMany(b => b.CreatedCommunities);

        builder.HasMany(b => b.CommunitySubscribers)
            .WithMany(b => b.CommunitySubscriptions);

        builder.Property(b => b.CreatedAt)
            .IsRequired(false);

        builder.Property(b => b.LastNameUpdatedAt)
            .IsRequired(false);
    }
}
