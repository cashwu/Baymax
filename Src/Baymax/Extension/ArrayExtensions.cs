namespace Baymax.Extension
{
    public static class ArrayExtensions
    {
        public static string ToJoinString(this string[] array, string separator)
        {
            return string.Join(separator, array);
        }
    }
}