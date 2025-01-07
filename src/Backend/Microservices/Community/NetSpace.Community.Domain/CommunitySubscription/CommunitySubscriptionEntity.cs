using NetSpace.Community.Domain.Community;
using NetSpace.Community.Domain.User;

namespace NetSpace.Community.Domain.CommunitySubscription;

public sealed class CommunitySubscriptionEntity : IEntity<int>
{
    public int Id { get; set; }

    public UserEntity Subscriber { get; set; }
    public Guid SubscriberId { get; set; }

    public CommunityEntity Community { get; set; }
    public int CommunityId { get; set; }

    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;

    public SubscribingStatus SubscribingStatus { get; set; } = SubscribingStatus.WaitForConfirmation;
}
