using System;
using System.Collections.Generic;

namespace Baymax.Extension
{
    public static class StringExtensions
    {
        public static List<string> SplitToList(this string str, char separator = ',')
        {
            if (string.IsNullOrEmpty(str))
            {
                return new List<string>();
            }

            return new List<string>(str.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries));
        }

        public static bool EqualsIgnoreCase(this string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }
    }
}