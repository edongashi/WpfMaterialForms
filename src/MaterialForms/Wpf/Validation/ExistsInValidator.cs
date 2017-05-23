using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class ExistsInValidator : ComparisonValidator
    {
        public ExistsInValidator(BindingProxy argument, StringProxy errorMessage) : base(argument, errorMessage)
        {
        }

        public ExistsInValidator(BindingProxy argument, StringProxy errorMessage, IValueConverter valueConverter) : base(argument, errorMessage, valueConverter)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            if (Argument.Value is IEnumerable<object> e)
            {
                return e.Contains(value);
            }

            return true;
        }
    }
}
