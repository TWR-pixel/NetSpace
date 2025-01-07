using NetSpace.Community.Application.Common.MediatR;
using NetSpace.Community.Application.Community;
using NetSpace.Community.Application.User;
using NetSpace.Community.Domain.CommunitySubscription;

namespace NetSpace.Community.Application.CommunitySubscription;

public sealed record CommunitySubscriptionResponse : ResponseBase
{
    public int Id { get; set; }

    public UserResponse? Subscriber { get; set; }
    public Guid SubscriberId { get; set; }

    public CommunityResponse? Community { get; set; }
    public int CommunityId { get; set; }

    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;

    public SubscribingStatus SubscribingStatus { get; set; } = SubscribingStatus.WaitForConfirmation;
}
