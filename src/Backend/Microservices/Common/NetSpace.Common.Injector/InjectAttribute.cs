using NetSpace.Common.Injector.Extensions;

namespace NetSpace.Common.Injector;

/// <summary>
/// Uses for registration services in <see cref="IServiceCollection"/>.
/// Method for registration is <see cref="InjectorServiceCollectionExtensions.RegisterInjectServicesFromAssembly(IServiceCollection, System.Reflection.Assembly)"/>
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class InjectAttribute : Attribute
{
    /// <summary>
    /// The <see cref="Type"/> for which this class will be registered as implementing it
    /// </summary>
    public Type? ImplementationFor { get; set; }

    /// <summary>
    /// Service type registration for service implementation. Default is <see cref="RegisterServiceType.Scoped"/>
    /// </summary>
    public RegisterServiceType RegisterServiceType { get; set; } = RegisterServiceType.Scoped;
}
