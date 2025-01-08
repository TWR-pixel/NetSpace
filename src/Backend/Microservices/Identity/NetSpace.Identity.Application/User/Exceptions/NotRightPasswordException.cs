namespace NetSpace.Identity.Application.User.Exceptions;

public sealed class NotRightPasswordException(string password) : Exception($"User's password '{password}' not right. ")
{
}
