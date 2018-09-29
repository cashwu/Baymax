using System;
using System.Collections.Generic;
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
                var configSection = type.GetCustomAttribute<ConfigSectionAttribute>();

                var t = type;

                if (configSection.IsCollections)
                {
                    t = typeof(List<>).MakeGenericType(configSection.CollectionType ?? type);
                }

                services.AddScoped(t, provider => configuration.GetSection(configSection.Name).Get(t));
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