namespace NetSpace.User.Application.Common.Exceptions;

public class NotFoundException(string? message) : Exception(message)
{
}
