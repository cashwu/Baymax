using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Baymax.Util
{
    public abstract class Enumeration<T> : IComparable where T : Enumeration<T>
    {
        protected Enumeration(int value, string displayName)
        {
            Value = value;
            DisplayName = displayName;
        }

        public int Value { get; }

        public string DisplayName { get; }

        public static IEnumerable<T> GetAll() 
        {
            return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                            .Select(p => (T) p.GetValue(default(T)));
        }

        public static T FromValue(int value) 
        {
            return Parse(item => item.Value == value);
        }

        public static T FromDisplayName(string displayName) 
        {
            return Parse(item => string.Equals(item.DisplayName, displayName, StringComparison.OrdinalIgnoreCase));
        }

        public int CompareTo(object obj)
        {
            return Value.CompareTo(((Enumeration<T>) obj).Value);
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration<T>;

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

        private static T Parse(Func<T, bool> predicate) 
        {
            return GetAll().FirstOrDefault(predicate);
        }
    }
}