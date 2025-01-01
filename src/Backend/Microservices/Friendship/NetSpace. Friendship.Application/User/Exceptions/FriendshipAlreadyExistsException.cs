namespace NetSpace.Friendship.Application.User.Exceptions;

public sealed class FriendshipAlreadyExistsException(Guid fromId, Guid toId) : Exception($"A user with id = '{fromId}' is already subscribed to a user with id = '{toId}'")
{
}
