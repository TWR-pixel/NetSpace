namespace NetSpace.Friendship.Application.User.Exceptions;

public sealed class UserNotFoundException(Guid id) : Exception($"User with id '{id}' not found.")
{
}
