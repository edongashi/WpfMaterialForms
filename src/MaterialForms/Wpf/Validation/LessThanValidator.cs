using System;
using System.Globalization;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class LessThanValidator : ComparisonValidator
    {
        public LessThanValidator(IProxy argument, IErrorStringProvider errorProvider, IBoolProxy isEnforced, IValueConverter valueConverter) : base(argument, errorProvider, isEnforced, valueConverter)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            var comparand = Argument.Value;
            if (comparand == null)
            {
                return true;
            }

            if (value == null)
            {
                return false;
            }

            if (value is IComparable c)
            {
                return c.CompareTo(comparand) < 0;
            }

            return false;
        }
    }
}
