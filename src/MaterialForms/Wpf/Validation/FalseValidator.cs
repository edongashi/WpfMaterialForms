using System.Globalization;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class FalseValidator : FieldValidator
    {
        public FalseValidator(IStringProxy errorMessage, IValueConverter valueConverter) : base(errorMessage, valueConverter)
        {
        }

        public FalseValidator(IStringProxy errorMessage) : base(errorMessage)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            return value is false;
        }
    }
}
