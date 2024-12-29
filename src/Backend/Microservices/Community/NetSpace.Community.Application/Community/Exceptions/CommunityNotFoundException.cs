namespace NetSpace.Community.Application.Community.Exceptions;

public sealed class CommunityNotFoundException(int id) : Exception($"Community with id '{id}' not found")
{
}
