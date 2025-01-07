using NetSpace.Community.Domain.CommunitySubscription;

namespace NetSpace.Community.UseCases.CommunitySubscription;

public sealed class CommunitySubscriptionFilterOptions
{
    public int? Id { get; set; }
    public Guid? SubscriberId { get; set; }
    public bool IncludeSubscriber { get; set; } = false;

    public int? CommunityId { get; set; }
    public bool IncludeCommunity { get; set; } = false;

    public DateTime? SubscribedAt { get; set; }

    public SubscribingStatus? SubscribingStatus { get; set; }
}
