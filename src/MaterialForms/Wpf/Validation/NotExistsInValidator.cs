using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class NotExistsInValidator : ComparisonValidator
    {
        public NotExistsInValidator(IProxy argument, IStringProxy errorProvider) : base(argument, errorProvider)
        {
        }

        public NotExistsInValidator(IProxy argument, IStringProxy errorProvider, IValueConverter valueConverter) : base(argument, errorProvider, valueConverter)
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
