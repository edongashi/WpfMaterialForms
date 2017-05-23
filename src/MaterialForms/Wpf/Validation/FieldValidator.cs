using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public abstract class FieldValidator : ValidationRule
    {
        public IValueConverter ValueConverter { get; }

        public StringProxy ErrorMessage { get; }

        protected FieldValidator(StringProxy errorMessage, IValueConverter valueConverter)
        {
            ErrorMessage = errorMessage;
            ValueConverter = valueConverter;
        }

        protected FieldValidator(StringProxy errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public sealed override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var isValid = ValidateValue(ValueConverter != null 
                ? ValueConverter.Convert(value, typeof(object), null, cultureInfo) 
                : value, cultureInfo);

            return isValid 
                ? new ValidationResult(true, null) 
                : new ValidationResult(false, ErrorMessage.Value);
        }

        protected abstract bool ValidateValue(object value, CultureInfo cultureInfo);
    }
}
