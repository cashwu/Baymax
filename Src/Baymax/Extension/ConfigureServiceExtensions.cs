using System.Reflection;
using Baymax.Attribute;
using Baymax.Model.Config;
using Baymax.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Baymax.Extension
{
    public static class ConfigureServiceExtensions
    {
        public static void AddDefaultConfigMapping(this IServiceCollection services, IConfiguration configuration)
        {
            var types = Reflection.GetAssembliesTypeOf<IConfig>();

            foreach (var type in types)
            {
                services.AddScoped(type, provider =>
                {
                    var configSection = type.GetCustomAttribute<ConfigSectionAttribute>();
                    return configuration.GetSection(configSection.Name).Get(type);
                });
            }
        }

        public static void AddRegisterAllType<T>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = Reflection.GetAssembliesTypeOf<T>();
            foreach (var type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
            }
        }
    }
}