using System;
using System.Globalization;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources.ValueConverters
{
    public class CoercedValueConverter<T> : IValueConverter
    {
        public CoercedValueConverter(T defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public T DefaultValue { get; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T ? value : DefaultValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
