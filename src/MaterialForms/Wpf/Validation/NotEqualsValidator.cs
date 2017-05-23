using System.Globalization;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class NotEqualsValidator : ComparisonValidator
    {
        public NotEqualsValidator(BindingProxy argument, StringProxy errorMessage) : base(argument, errorMessage)
        {
        }

        public NotEqualsValidator(BindingProxy argument, StringProxy errorMessage, IValueConverter valueConverter) : base(argument, errorMessage, valueConverter)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            return !Equals(Argument.Value, value);
        }
    }
}
