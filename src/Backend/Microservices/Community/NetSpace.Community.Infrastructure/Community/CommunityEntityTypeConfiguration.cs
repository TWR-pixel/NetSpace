using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetSpace.Community.Domain.Community;

namespace NetSpace.Community.Infrastructure.Community;

public sealed class CommunityEntityTypeConfiguration : IEntityTypeConfiguration<CommunityEntity>
{
    public void Configure(EntityTypeBuilder<CommunityEntity> builder)
    {
        builder
            .HasOne(b => b.Owner)
            .WithMany(b => b.CreatedCommunities)
            .HasForeignKey(b => b.OwnerId);

        builder
            .HasMany(b => b.CommunitySubscribers)
            .WithMany(b => b.CommunitySubscriptions);
    }
}
