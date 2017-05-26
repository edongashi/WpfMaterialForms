using System.Globalization;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class TrueValidator : FieldValidator
    {
        public TrueValidator(IErrorStringProvider errorProvider, IBoolProxy isEnforced, IValueConverter valueConverter)
            : base(errorProvider, isEnforced, valueConverter)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            return value is true;
        }
    }
}
