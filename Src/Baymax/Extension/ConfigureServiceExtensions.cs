using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Baymax.Attribute;
using Baymax.Entity;
using Baymax.Model.Config;
using Baymax.Services;
using Baymax.Services.Interface;
using Baymax.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Baymax.Extension
{
    public static class ConfigureServiceExtensions
    {
        public static void AddEntityValidation<TEntity>(this IServiceCollection services, Func<object, ValidationResult> checkFunc)
                where TEntity : BaseEntity
        {
            EntityValidation.SetProcessRoutines(typeof(TEntity), checkFunc);
        }

        public static void AddBackgroundService(this IServiceCollection services, params Type[] type)
        {
            foreach (var t in type)
            {
                if (t.GetInterfaces().First() != typeof(IBackgroundProcessService))
                {
                    throw new ArgumentException($"Not implement type {nameof(IBackgroundProcessService)}");
                }

                services.AddScoped(t);
                services.AddSingleton(typeof(IHostedService), typeof(BaymaxBackgroundService<>).MakeGenericType(t));
            }
        }

        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration, string prefixAssemblyName)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var types = Reflection.GetAssembliesTypeOf<IConfig>(prefixAssemblyName);

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

            return services;
        }

        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddConfig(configuration, string.Empty);
        }

        public static IServiceCollection AddLogService(this IServiceCollection services, string prefixAssemblyName = "")
        {
            services.AddTypeOf<ILogBase>(prefixAssemblyName);
            services.AddScoped<ILogService, LogService>();

            return services;
        }

        public static void AddTypeOf<T>(this IServiceCollection services, string prefixAssemblyName = "", ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var typesFromAssemblies = Reflection.GetAssembliesTypeOf<T>(prefixAssemblyName);
            foreach (var type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
            }
        }

        public static IServiceCollection AddGeneralService(this IServiceCollection services, string prefixAssemblyName, Dictionary<Type, ServiceLifetime> customerServiceLifetime = null)
        {
            if (string.IsNullOrEmpty(prefixAssemblyName))
            {
                throw new ArgumentNullException(nameof(prefixAssemblyName));
            }

            services.AddTypeFor(prefixAssemblyName, f => f.Name.EndsWith("Service"), customerServiceLifetime);

            return services;
        }

        private static void AddTypeFor(this IServiceCollection services,
                                       string prefixAssemblyName,
                                       Func<TypeInfo, bool> typeNameCondition,
                                       Dictionary<Type, ServiceLifetime> customerServiceLifetime)
        {
            var assemblies = Reflection.GetAssembliesTypeOf(prefixAssemblyName, typeNameCondition);

            foreach (var @class in assemblies)
            {
                foreach (var @interface in @class.GetInterfaces().Where(a => typeNameCondition.Invoke(a.GetTypeInfo())))
                {
                    if (@interface == typeof(IBackgroundProcessService))
                    {
                        continue;
                    }

                    var lifeTime = TypeServiceLifetime(customerServiceLifetime, @class, @interface);

                    services.Add(new ServiceDescriptor(@interface, @class.AsType(), lifeTime));
                }
            }
        }

        private static ServiceLifetime TypeServiceLifetime(Dictionary<Type, ServiceLifetime> customerServiceLifetime, TypeInfo @class, Type @interface)
        {
            if (customerServiceLifetime == null)
            {
                return ServiceLifetime.Scoped;
            }
            
            if (customerServiceLifetime.ContainsKey(@class))
            {
                return customerServiceLifetime[@class];
            }

            if (customerServiceLifetime.ContainsKey(@interface))
            {
                return customerServiceLifetime[@interface];
            }

            return ServiceLifetime.Scoped;
        }
    }
}