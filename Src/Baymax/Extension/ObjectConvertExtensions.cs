using System;

namespace Baymax.Extension
{
    public static class ObjectConvertExtensions
    {
        public static string ToNotNullString(this object me, string defaultValue)
        {
            var result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            if (me is string str)
            {
                result = str;
            }
            else
            {
                result = me.ToString();
            }

            return result;
        }

        public static string ToNotNullString(this object me)
        {
            return me.ToNotNullString(string.Empty);
        }

        public static char ToChar(this object me, char defaultValue)
        {
            var result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            if (me is string s)
            {
                if (!char.TryParse(s, out result))
                {
                    result = defaultValue;
                }
            }
            else if (me is IConvertible)
            {
                result = Convert.ToChar(me);
            }

            return result;
        }

        public static char ToChar(this object me)
        {
            return me.ToChar(default(char));
        }

        public static byte ToByte(this object me, byte defaultValue)
        {
            var result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            if (me is string s)
            {
                if (!byte.TryParse(s, out result))
                {
                    result = defaultValue;
                }
            }
            else if (me is IConvertible)
            {
                result = Convert.ToByte(me);
            }

            return result;
        }

        public static byte ToByte(this object me)
        {
            return me.ToByte(default(byte));
        }

        public static sbyte ToSByte(this object me, sbyte defaultValue)
        {
            var result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            if (me is string s)
            {
                if (!sbyte.TryParse(s, out result))
                {
                    result = defaultValue;
                }
            }
            else if (me is IConvertible)
            {
                result = Convert.ToSByte(me);
            }

            return result;
        }

        public static sbyte ToSByte(this object me)
        {
            return me.ToSByte(default(sbyte));
        }
        
        public static short ToShort(this object me, short defaultValue)
        {
            var result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            if (me is string s)
            {
                if (!short.TryParse(s, out result))
                {
                    result = defaultValue;
                }
            }
            else if (me is IConvertible)
            {
                result = Convert.ToInt16(me);
            }

            return result;
        }

        public static short ToShort(this object me)
        {
            return me.ToShort(default(short));
        }

        public static ushort ToUShort(this object me, ushort defaultValue)
        {
            var result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            if (me is string s)
            {
                if (!ushort.TryParse(s, out result))
                {
                    result = defaultValue;
                }
            }
            else if (me is IConvertible)
            {
                result = Convert.ToUInt16(me);
            }

            return result;
        }

        public static ushort ToUShort(this object me)
        {
            return me.ToUShort(default(ushort));
        }

        public static int ToInt(this object me, int defaultValue)
        {
            var result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            if (me is string s)
            {
                if (!int.TryParse(s, out result))
                {
                    result = defaultValue;
                }
            }
            else if (me is IConvertible)
            {
                result = Convert.ToInt32(me);
            }

            return result;
        }

        public static int ToInt(this object me)
        {
            return me.ToInt(default(int));
        }

        public static uint ToUInt(this object me, uint defaultValue)
        {
            var result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            if (me is string s)
            {
                if (!uint.TryParse(s, out result))
                {
                    result = defaultValue;
                }
            }
            else if (me is IConvertible)
            {
                result = Convert.ToUInt32(me);
            }

            return result;
        }

        public static uint ToUInt(this object me)
        {
            return me.ToUInt(default(uint));
        }

        public static long ToLong(this object me, long defaultValue)
        {
            var result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            if (me is string s)
            {
                if (!long.TryParse(s, out result))
                {
                    result = defaultValue;
                }
            }
            else if (me is IConvertible)
            {
                result = Convert.ToInt64(me);
            }

            return result;
        }

        public static long ToLong(this object me)
        {
            return me.ToLong(default(long));
        }

        public static ulong ToULong(this object me, ulong defaultValue)
        {
            var result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            if (me is string s)
            {
                if (!ulong.TryParse(s, out result))
                {
                    result = defaultValue;
                }
            }
            else if (me is IConvertible)
            {
                result = Convert.ToUInt64(me);
            }

            return result;
        }

        public static ulong ToULong(this object me)
        {
            return me.ToULong(default(ulong));
        }

        public static decimal ToDecimal(this object me, decimal defaultValue)
        {
            var result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            if (me is string s)
            {
                if (!decimal.TryParse(s, out result))
                {
                    result = defaultValue;
                }
            }
            else if (me is IConvertible)
            {
                result = Convert.ToDecimal(me);
            }

            return result;
        }

        public static decimal ToDecimal(this object me)
        {
            return me.ToDecimal(default(decimal));
        }

        public static float ToFloat(this object me, float defaultValue)
        {
            var result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            if (me is string s)
            {
                if (!float.TryParse(s, out result))
                {
                    result = defaultValue;
                }
            }
            else if (me is IConvertible)
            {
                result = Convert.ToSingle(me);
            }

            return result;
        }

        public static float ToFloat(this object me)
        {
            return me.ToFloat(default(float));
        }

        public static double ToDouble(this object me, double defaultValue)
        {
            var result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            if (me is string s)
            {
                if (!double.TryParse(s, out result))
                {
                    result = defaultValue;
                }
            }
            else if (me is IConvertible)
            {
                result = Convert.ToDouble(me);
            }

            return result;
        }

        public static double ToDouble(this object me)
        {
            return me.ToDouble(default(double));
        }

        public static DateTime ToDateTime(this object me, DateTime defaultValue)
        {
            var result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            if (me is DateTime time)
            {
                result = time;
            }
            else if (!DateTime.TryParse(me.ToString(), out result))
            {
                result = defaultValue;
            }

            return result;
        }

        public static DateTime ToDateTime(this object me)
        {
            return me.ToDateTime(default(DateTime));
        }

        public static bool ToBool(this object me, bool defaultValue)
        {
            var result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            if (me is bool b)
            {
                result = b;
            }
            else if (me.IsNumeric())
            {
                result = me.ToDecimal() != decimal.Zero ? true : defaultValue;
            }
            else if (me is string)
            {
                if (!bool.TryParse(me.ToString(), out result))
                {
                    result = defaultValue;
                }
            }
            else
            {
                result = true;
            }

            return result;
        }

        public static bool ToBool(this object me)
        {
            return me.ToBool(false);
        }

        public static TEnum ToEnum<TEnum>(this object me, TEnum defaultValue) where TEnum : struct
        {
            TEnum result = defaultValue;

            if (me == null || me == DBNull.Value)
            {
                return result;
            }

            var myType = me.GetType();
            var destinationType = typeof(TEnum);

            if (myType.IsEnum && myType != destinationType)
            {
                me = me.ToString();
            }

            if (me is int)
            {
                if (Enum.IsDefined(typeof(TEnum), me))
                {
                    result = (TEnum) me;
                }
            }
            else if (!Enum.TryParse(me.ToString(), out result))
            {
                result = defaultValue;
            }

            return result;
        }

        public static TEnum ToEnum<TEnum>(this object me) where TEnum : struct
        {
            return me.ToEnum(default(TEnum));
        }
    }
}