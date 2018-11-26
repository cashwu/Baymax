using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Baymax.Util
{
    public abstract class Enumeration : IComparable
    {
        protected Enumeration()
        {
        }

        protected Enumeration(int value, string displayName)
        {
            Value = value;
            DisplayName = displayName;
        }

        public int Value { get; protected set; }

        public string DisplayName { get; protected set; }

        protected static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
        {
            return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                            .Select(p => (T) p.GetValue(default(T)));
        }

        protected static T FromValue<T>(int value) where T : Enumeration, new()
        {
            return Parse<T>(item => item.Value == value);
        }

        protected static T FromDisplayName<T>(string displayName) where T : Enumeration, new()
        {
            return Parse<T>(item => string.Equals(item.DisplayName, displayName, StringComparison.OrdinalIgnoreCase));
        }
        
        public int CompareTo(object obj)
        {
            return Value.CompareTo(((Enumeration) obj).Value);
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        private static T Parse<T>(Func<T, bool> predicate) where T : Enumeration, new()
        {
            return GetAll<T>().FirstOrDefault(predicate);
        }
    }
}