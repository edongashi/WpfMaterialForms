using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class FalseValidator : FieldValidator
    {
        public FalseValidator(IErrorStringProvider errorProvider, IBoolProxy isEnforced, IValueConverter valueConverter,
            ValidationStep validationStep, bool validatesOnTargetUpdated)
            : base(errorProvider, isEnforced, valueConverter, validationStep, validatesOnTargetUpdated)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            return value is false;
        }
    }
}
