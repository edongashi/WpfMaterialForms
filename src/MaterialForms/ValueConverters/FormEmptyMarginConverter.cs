using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MaterialForms.ValueConverters
{
    internal class FormEmptyMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var form = value as MaterialForm;
            return form != null && form.Count != 0
                ? new Thickness(0d, 0d, 0d, 8d)
                : new Thickness(0d, 0d, 0d, 4d);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
