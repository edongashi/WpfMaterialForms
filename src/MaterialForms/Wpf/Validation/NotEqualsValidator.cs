using System.Globalization;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class NotEqualsValidator : ComparisonValidator
    {
        public NotEqualsValidator(IProxy argument, IErrorStringProvider errorProvider) : base(argument, errorProvider)
        {
        }

        public NotEqualsValidator(IProxy argument, IErrorStringProvider errorProvider, IValueConverter valueConverter) : base(argument, errorProvider, valueConverter)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            return !Equals(Argument.Value, value);
        }
    }
}
