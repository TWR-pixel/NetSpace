using NetSpace.User.Application.Common.Exceptions;

namespace NetSpace.User.Application.User.Exceptions;

public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string email) : base($"User with email '{email}' not found.")
    {

    }

    public UserNotFoundException(Guid id) : base($"User with id '{id}' not found.")
    {

    }
}
