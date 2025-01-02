using NetSpace.User.Application.Common.Exceptions;

namespace NetSpace.User.Application.UserPostUserComment.Exceptions;

public sealed class UserPostUserCommentNotFoundException(int id) : NotFoundException($"User comment with id '{id}' not found.")
{
}
