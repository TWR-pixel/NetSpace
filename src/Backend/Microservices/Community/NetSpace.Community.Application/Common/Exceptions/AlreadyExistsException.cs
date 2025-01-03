namespace NetSpace.Community.Application.Common.Exceptions;

public class AlreadyExistsException(string? message) : Exception(message)
{
}
