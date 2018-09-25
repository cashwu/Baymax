using System.Collections.Generic;
using System.ComponentModel;

namespace Baymax.Extension
{
    public static class ObjectReflectionExtensions
    {
        public static IEnumerable<PropertyDescriptor> GetProperties(this object me)
        {
            var properties = TypeDescriptor.GetProperties(me);

            var result = new PropertyDescriptor[properties.Count];

            for (var index = 0; index < properties.Count; index++)
            {
                result[index] = properties[index];
            }

            return result;
        }
        
        public static bool IsNumeric(this object me)
        {
            return me is byte ||
                   me is short ||
                   me is int ||
                   me is long ||
                   me is sbyte ||
                   me is ushort ||
                   me is uint ||
                   me is ulong ||
                   me is decimal ||
                   me is double ||
                   me is float;
        }
    }
}