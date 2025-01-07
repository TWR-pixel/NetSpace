using NetSpace.Community.Application.Common.Exceptions;

namespace NetSpace.Community.Application.CommunityPostUserComment.Exceptions;

public sealed class CommunityPostUserCommentNotFoundException(int id) : NotFoundException($"Comment with id '{id}' not found")
{
}
