namespace NetSpace.Common.Injector;

/// <summary>
/// Types for registration services in DI container
/// </summary>
public enum RegisterServiceType : byte
{
    Transient,
    Scoped,
    Singleton
}
