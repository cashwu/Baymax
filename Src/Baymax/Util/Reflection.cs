using System;
using System.Collections.Generic;
using System.Linq;

namespace Baymax.Util
{
    public static class Reflection
    {
        public static IEnumerable<Type> GetAssembliesTypeOf<T>()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic);
            return assembly.SelectMany(a => a.DefinedTypes.Where(t => t.GetInterfaces().Contains(typeof(T))));
        }
    }
}