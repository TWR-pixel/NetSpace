namespace NetSpace.Identity.Application.User.Exceptions;

public sealed class UserAlreadyExistsException(string? email) : Exception($"User with email '{email}' already exists.")
{
}
