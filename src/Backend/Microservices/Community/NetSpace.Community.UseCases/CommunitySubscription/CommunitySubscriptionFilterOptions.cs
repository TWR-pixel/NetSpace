namespace NetSpace.Community.UseCases.CommunitySubscription;

public sealed class CommunitySubscriptionFilterOptions
{
    public int? Id { get; set; }
    public Guid? SubscriberId { get; set; }
    public bool IncludeSubscriber { get; set; } = false;

    public int? CommunityId { get; set; }
    public bool IncludeCommunity { get; set; } = false;
}
