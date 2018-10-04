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

        public static void AddBackgroundServiceFor(this IServiceCollection services, params Type[] type)
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

        public static void AddDefaultConfigMapping(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(IConfiguration));
            }

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

        public static void AddRegisterServiceTypeOf(this IServiceCollection services, string prefixAssemblyName, Dictionary<Type, ServiceLifetime> customerServiceLifetime = null)
        {
            if (string.IsNullOrEmpty(prefixAssemblyName))
            {
                throw new ArgumentNullException(nameof(prefixAssemblyName));
            }

            services.AddRegisterAllTypeFor(f => f.FullName.StartsWith(prefixAssemblyName),
                                           f => f.Name.EndsWith("Service"),
                                           customerServiceLifetime);
        }

        private static void AddRegisterAllTypeFor(this IServiceCollection services,
                                                  Func<Assembly, bool> assemblyCondition,
                                                  Func<TypeInfo, bool> typeNameCondition,
                                                  Dictionary<Type, ServiceLifetime> customerServiceLifetime)
        {
            var assemblies = Reflection.GetAssembliesTypeOf(assemblyCondition, typeNameCondition);

            foreach (var @class in assemblies)
            {
                foreach (var @interface in @class.GetInterfaces().Where(a => typeNameCondition.Invoke(a.GetTypeInfo())))
                {
                    if (@interface == typeof(IBackgroundProcessService))
                    {
                        continue;
                    }

                    var lifeTime = ServiceLifetime.Scoped;
                    if (customerServiceLifetime != null)
                    {
                        if (customerServiceLifetime.ContainsKey(@class))
                        {
                            lifeTime = customerServiceLifetime[@class];
                        }

                        if (customerServiceLifetime.ContainsKey(@interface))
                        {
                            lifeTime = customerServiceLifetime[@interface];
                        }
                    }
                    
                    services.Add(new ServiceDescriptor(@interface, @class.AsType(), lifeTime));
                }
            }
        }
    }
}