namespace NetSpace.User.Application.User.Exceptions;

public sealed class NotRightPasswordException : Exception
{
    public NotRightPasswordException(string password) : base($"User password '{password}' not right.")
    {

    }
}
