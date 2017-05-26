using System.Globalization;
using System.Windows.Data;

namespace MaterialForms.Wpf.Validation
{
    public class NullValidator : FieldValidator
    {
        public NullValidator(IErrorStringProvider errorProvider, IValueConverter valueConverter) : base(errorProvider, valueConverter)
        {
        }

        public NullValidator(IErrorStringProvider errorProvider) : base(errorProvider)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            return value == null;
        }
    }
}
