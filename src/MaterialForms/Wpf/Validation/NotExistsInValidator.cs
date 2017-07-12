using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class NotExistsInValidator : ComparisonValidator
    {
        public NotExistsInValidator(IProxy argument, IErrorStringProvider errorProvider, IBoolProxy isEnforced,
            IValueConverter valueConverter, ValidationStep validationStep, bool validatesOnTargetUpdated)
            : base(argument, errorProvider, isEnforced, valueConverter, validationStep, validatesOnTargetUpdated)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            if (Argument.Value is IEnumerable<object> e)
            {
                return !e.Contains(value);
            }

            return true;
        }
    }
}
