using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Baymax.Attribute;
using Baymax.Model.Config;
using Baymax.Services;
using Baymax.Services.Interface;
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

                services.Add(new ServiceDescriptor(t, provider => configuration.GetSection(configSection.Name).Get(t), configSection.Lifetime));
            }
        }

        public static void AddLogService(this IServiceCollection services)
        {
            services.AddRegisterAllType<ILogBase>();
            services.AddScoped<ILogService, LogService>();
        }

        public static void AddRegisterAllType<T>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var typesFromAssemblies = Reflection.GetAssembliesTypeOf<T>();
            foreach (var type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
            }
        }

        public static void AddRegisterAllTypeFor(this IServiceCollection services,
                                                 Func<Assembly, bool> assemblyCondition,
                                                 Func<TypeInfo, bool> typeNameCondition,
                                                 Dictionary<string, ServiceLifetime> typeLifetimeLookup)
        {
            if (typeNameCondition == null)
            {
                return;
            }

            var assemblies = Reflection.GetAssembliesTypeOf(assemblyCondition, typeNameCondition);
            foreach (var @class in assemblies)
            {
                foreach (var @interface in @class.GetInterfaces().Where(a => typeNameCondition.Invoke(a.GetTypeInfo())))
                {
                    var lifetime = ServiceLifetime.Scoped;
                    if (typeLifetimeLookup.ContainsKey(@class.Name))
                    {
                        lifetime = typeLifetimeLookup[@class.Name];
                    }

                    services.Add(new ServiceDescriptor(@interface, @class.AsType(), lifetime));
                }
            }
        }
    }
}