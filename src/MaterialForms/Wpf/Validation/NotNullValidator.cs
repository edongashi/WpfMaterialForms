using System.Globalization;
using System.Windows.Data;

namespace MaterialForms.Wpf.Validation
{
    public class NotNullValidator : FieldValidator
    {
        public NotNullValidator(IErrorStringProvider errorProvider, IValueConverter valueConverter) : base(errorProvider, valueConverter)
        {
        }

        public NotNullValidator(IErrorStringProvider errorProvider) : base(errorProvider)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            return value != null;
        }
    }
}
