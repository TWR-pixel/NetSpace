using NetSpace.Community.Application.Common.Exceptions;

namespace NetSpace.Community.Application.CommunitySubscription.Exceptions;

public sealed class CommunitySubscriptionNotFoundException(int id) : NotFoundException($"Subscription with id '{id}' not found.")
{
}
