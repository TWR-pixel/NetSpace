using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetSpace.Community.Domain.Community;

namespace NetSpace.Community.Infrastructure.Common;

public sealed class CommunityEntityTypeConfiguration : IEntityTypeConfiguration<CommunityEntity>
{
    public void Configure(EntityTypeBuilder<CommunityEntity> builder)
    {

    }
}
