using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetSpace.Community.Domain.CommunitySubscription;

namespace NetSpace.Community.Infrastructure.CommunitySubscription;

public sealed class CommunitySubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<CommunitySubscriptionEntity>
{
    public void Configure(EntityTypeBuilder<CommunitySubscriptionEntity> builder)
    {

    }
}
