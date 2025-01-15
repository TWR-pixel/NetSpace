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

        foreach (var serviceType in types)
        {
            var attributeValue = serviceType.GetCustomAttribute<InjectAttribute>();

            if (attributeValue is null || attributeValue.ImplementationsFor is null)
            {
                continue;
            }

            foreach (var serviceAbstraction in attributeValue.ImplementationsFor)
            {
                if (attributeValue.RegisterServiceType == RegisterServiceType.Transient)
                    services.AddTransient(serviceAbstraction, serviceType);

                else if (attributeValue.RegisterServiceType == RegisterServiceType.Scoped)
                    services.AddScoped(serviceAbstraction, serviceType);

                else if (attributeValue.RegisterServiceType == RegisterServiceType.Singleton)
                    services.AddSingleton(serviceAbstraction, serviceType);
            }
        }

        return services;
    }
}
