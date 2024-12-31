﻿namespace NetSpace.EmailSender.Tests.Unit.Application;

public static class AssertHelper
{
    public static void DoesntThrow(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            Assert.Fail($"Exception was thrown: {ex.Message}");
        }
    }
}
