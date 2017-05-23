using System.Globalization;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class TrueValidator : FieldValidator
    {
        public TrueValidator(IStringProxy errorMessage, IValueConverter valueConverter) : base(errorMessage, valueConverter)
        {
        }

        public TrueValidator(IStringProxy errorMessage) : base(errorMessage)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            return value is true;
        }
    }
}
