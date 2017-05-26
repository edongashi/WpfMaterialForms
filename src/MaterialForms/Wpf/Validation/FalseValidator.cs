using System.Globalization;
using System.Windows.Data;

namespace MaterialForms.Wpf.Validation
{
    public class FalseValidator : FieldValidator
    {
        public FalseValidator(IErrorStringProvider errorProvider, IValueConverter valueConverter) : base(errorProvider, valueConverter)
        {
        }

        public FalseValidator(IErrorStringProvider errorProvider) : base(errorProvider)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            return value is false;
        }
    }
}
