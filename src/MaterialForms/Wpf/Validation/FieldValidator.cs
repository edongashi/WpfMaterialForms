using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialForms.Wpf.Validation
{
    public abstract class FieldValidator : ValidationRule
    {
        public IValueConverter ValueConverter { get; }

        public IErrorStringProvider ErrorProvider { get; }

        protected FieldValidator(IErrorStringProvider errorProvider, IValueConverter valueConverter)
        {
            ErrorProvider = errorProvider;
            ValueConverter = valueConverter;
        }

        protected FieldValidator(IErrorStringProvider errorProvider)
        {
            ErrorProvider = errorProvider;
        }

        public sealed override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var isValid = ValidateValue(ValueConverter != null 
                ? ValueConverter.Convert(value, typeof(object), null, cultureInfo) 
                : value, cultureInfo);

            return isValid 
                ? new ValidationResult(true, null) 
                : new ValidationResult(false, ErrorProvider.GetErrorMessage(value));
        }

        protected abstract bool ValidateValue(object value, CultureInfo cultureInfo);
    }
}
