namespace NetSpace.Community.Application.Common.Exceptions;

public sealed class UserNotFoundException(Guid id) : Exception($"User with id '{id}' not found")
{
}
