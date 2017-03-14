using System;
using System.Globalization;
using System.Windows.Data;

namespace MaterialForms.ValueConverters
{
    internal class ValueToPercentConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = System.Convert.ToDouble(values[0]);
            var maximum = System.Convert.ToDouble(values[1]);
            return Math.Round(100d * value / maximum) + "%";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new[] { Binding.DoNothing };
        }
    }
}
