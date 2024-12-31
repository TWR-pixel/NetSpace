using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetSpace.Community.Domain.Community;

namespace NetSpace.Community.Infrastructure.Common;

public sealed class CommunityEntityTypeConfiguration : IEntityTypeConfiguration<CommunityEntity>
{
    public void Configure(EntityTypeBuilder<CommunityEntity> builder)
    {
        builder.HasOne(c => c.Owner)
            .WithMany(c => c.CreatedCommunities)
            .HasForeignKey(c => c.OwnerId);


    }
}
