using System.Globalization;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    internal class EqualsValidator : ComparisonValidator
    {
        public EqualsValidator(IProxy argument, IStringProxy errorProvider)
            : base(argument, errorProvider)
        {
        }

        public EqualsValidator(IProxy argument, IStringProxy errorProvider, IValueConverter valueConverter)
            : base(argument, errorProvider, valueConverter)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            return Equals(Argument.Value, value);
        }
    }
}
