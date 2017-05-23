using System.Globalization;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class NullValidator : FieldValidator
    {
        public NullValidator(IStringProxy errorMessage, IValueConverter valueConverter) : base(errorMessage, valueConverter)
        {
        }

        public NullValidator(IStringProxy errorMessage) : base(errorMessage)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            return value == null;
        }
    }
}
