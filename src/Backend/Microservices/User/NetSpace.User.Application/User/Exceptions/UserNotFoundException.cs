﻿namespace NetSpace.User.Application.User.Exceptions;

public sealed class UserNotFoundException : Exception
{
    public UserNotFoundException(string email) : base($"User with email '{email}' not found.")
    {
        
    }

    public UserNotFoundException(int id) : base($"User with id '{id}' not found.")
    {

    }
}
