using System;
using System.Globalization;
using System.Windows.Data;

namespace MaterialForms.ValueConverters
{
    public class StringToIntegerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result;
            if (int.TryParse(value?.ToString(), out result))
            {
                return result;
            }

            return null;
        }
    }
}
