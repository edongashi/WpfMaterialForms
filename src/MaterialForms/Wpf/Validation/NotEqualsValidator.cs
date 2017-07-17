using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class NotEqualsValidator : ComparisonValidator
    {
        public NotEqualsValidator(IProxy argument, IErrorStringProvider errorProvider, IBoolProxy isEnforced,
            IValueConverter valueConverter, ValidationStep validationStep, bool validatesOnTargetUpdated)
            : base(argument, errorProvider, isEnforced, valueConverter, validationStep, validatesOnTargetUpdated)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            var comparand = Argument.Value;
            if (value.GetType() != comparand.GetType() && comparand is IConvertible)
            {
                comparand = Convert.ChangeType(comparand, value.GetType(), CultureInfo.InvariantCulture);
            }

            return !Equals(comparand, value);
        }
    }
}
