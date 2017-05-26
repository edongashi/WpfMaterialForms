using System.Globalization;
using System.Windows.Data;

namespace MaterialForms.Wpf.Validation
{
    public class TrueValidator : FieldValidator
    {
        public TrueValidator(IErrorStringProvider errorProvider, IValueConverter valueConverter) : base(errorProvider, valueConverter)
        {
        }

        public TrueValidator(IErrorStringProvider errorProvider) : base(errorProvider)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            return value is true;
        }
    }
}
