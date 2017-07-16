using System;
using System.Globalization;

namespace MaterialForms.Wpf.FormBuilding
{
    public static class Deserializers
    {
        public static object String(string expression)
        {
            return expression;
        }

        public static object DateTime(string expression)
        {
            return System.DateTime.Parse(expression, CultureInfo.InvariantCulture);
        }

        public static object Boolean(string expression)
        {
            return System.Boolean.Parse(expression);
        }

        public static object Char(string expression)
        {
            return System.Char.Parse(expression);
        }

        public static object Byte(string expression)
        {
            return System.Byte.Parse(expression, CultureInfo.InvariantCulture);
        }

        public static object SByte(string expression)
        {
            return System.SByte.Parse(expression, CultureInfo.InvariantCulture);
        }

        public static object Int16(string expression)
        {
            return System.Int16.Parse(expression, CultureInfo.InvariantCulture);
        }

        public static object Int32(string expression)
        {
            return System.Int32.Parse(expression, CultureInfo.InvariantCulture);
        }

        public static object Int64(string expression)
        {
            return System.Int64.Parse(expression, CultureInfo.InvariantCulture);
        }


        public static object UInt16(string expression)
        {
            return System.UInt16.Parse(expression, CultureInfo.InvariantCulture);
        }

        public static object UInt32(string expression)
        {
            return System.UInt32.Parse(expression, CultureInfo.InvariantCulture);
        }

        public static object UInt64(string expression)
        {
            return System.UInt64.Parse(expression, CultureInfo.InvariantCulture);
        }

        public static object Single(string expression)
        {
            return System.Single.Parse(expression, CultureInfo.InvariantCulture);
        }

        public static object Double(string expression)
        {
            return System.Double.Parse(expression, CultureInfo.InvariantCulture);
        }

        public static object Decimal(string expression)
        {
            return System.Decimal.Parse(expression, CultureInfo.InvariantCulture);
        }

        public static Func<string, object> Enum<TEnum>()
        {
            return Enum(typeof(TEnum));
        }

        public static Func<string, object> Enum(Type enumType)
        {
            return expr => System.Enum.Parse(enumType, expr);
        }
    }
}
