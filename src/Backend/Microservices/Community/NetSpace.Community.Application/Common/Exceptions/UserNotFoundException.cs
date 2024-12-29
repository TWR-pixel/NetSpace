namespace NetSpace.Community.Application.Common.Exceptions;

public sealed class UserNotFoundException(string id) : Exception($"User with id '{id}' not found")
{
}
