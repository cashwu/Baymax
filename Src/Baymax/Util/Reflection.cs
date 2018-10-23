using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Baymax.Util
{
    public static class Reflection
    {
        public static IEnumerable<Type> GetAssembliesTypeOf<T>(string prefixAssemblyName)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic);

            if (!string.IsNullOrEmpty(prefixAssemblyName))
            {
                return assembly.Where(a => a.FullName.StartsWith(prefixAssemblyName))
                               .SelectMany(a => a.DefinedTypes.Where(t => t.GetInterfaces().Contains(typeof(T))));
            }

            return assembly.SelectMany(a => a.DefinedTypes.Where(t => t.GetInterfaces().Contains(typeof(T))));
        }

        public static IEnumerable<TypeInfo> GetAssembliesTypeOf(string prefixAssemblyName, Func<TypeInfo, bool> typeCondition)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic && a.FullName.StartsWith(prefixAssemblyName));

            return assembly.SelectMany(a => a.DefinedTypes
                                             .Where(t => t.IsClass)
                                             .Where(typeCondition));
        }
    }
}