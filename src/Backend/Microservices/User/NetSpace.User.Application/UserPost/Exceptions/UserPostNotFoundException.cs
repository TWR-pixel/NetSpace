using NetSpace.User.Application.Common.Exceptions;

namespace NetSpace.User.Application.UserPost.Exceptions;

public sealed class UserPostNotFoundException(int id) : NotFoundException($"User post with id '{id}' not found.")
{
}
