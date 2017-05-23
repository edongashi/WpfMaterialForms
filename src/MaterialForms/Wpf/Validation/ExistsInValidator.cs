using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class ExistsInValidator : ComparisonValidator
    {
        public ExistsInValidator(IProxy argument, IStringProxy errorMessage) : base(argument, errorMessage)
        {
        }

        public ExistsInValidator(IProxy argument, IStringProxy errorMessage, IValueConverter valueConverter) : base(argument, errorMessage, valueConverter)
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
