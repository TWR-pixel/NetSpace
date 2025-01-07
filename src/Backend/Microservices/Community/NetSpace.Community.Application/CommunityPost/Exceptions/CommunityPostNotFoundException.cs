using NetSpace.Community.Application.Common.Exceptions;

namespace NetSpace.Community.Application.CommunityPost.Exceptions;

public sealed class CommunityPostNotFoundException(int id) : NotFoundException($"Community post with id '{id}' not found.")
{

}
