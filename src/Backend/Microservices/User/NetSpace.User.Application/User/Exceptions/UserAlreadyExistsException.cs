using NetSpace.User.Application.Common.Exceptions;

namespace NetSpace.User.Application.User.Exceptions;

public sealed class UserAlreadyExistsException : AlreadyExistsException
{
    public UserAlreadyExistsException(string email) : base($"User with email '{email}' already exists")
    {

    }


}
