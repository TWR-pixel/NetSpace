using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace NetSpace.Common.Injector.Extensions;

public static class InjectorServiceCollectionExtensions
{
    /// <summary>
    /// Register all classes with <see cref="InjectAttribute"/>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterInjectServicesFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes();

        foreach (var type in types)
        {
            var attributeValue = type.GetCustomAttribute<InjectAttribute>();

            if (attributeValue is null || attributeValue.ImplementationFor is null)
            {
                continue;
            }

            if (attributeValue.RegisterServiceType == RegisterServiceType.Transient)
                services.AddTransient(type, attributeValue.ImplementationFor);


            else if (attributeValue.RegisterServiceType == RegisterServiceType.Scoped)
                services.AddScoped(attributeValue.ImplementationFor, type);


            else if (attributeValue.RegisterServiceType == RegisterServiceType.Singleton)
                services.AddSingleton(type, attributeValue.ImplementationFor);

        }

        return services;
    }
}
