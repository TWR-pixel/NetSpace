using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetSpace.Community.Domain.CommunityPost;

namespace NetSpace.Community.Infrastructure.CommunityPost;

public sealed class CommunityPostEntityTypeConfiguration : IEntityTypeConfiguration<CommunityPostEntity>
{
    public void Configure(EntityTypeBuilder<CommunityPostEntity> builder)
    {

    }
}
