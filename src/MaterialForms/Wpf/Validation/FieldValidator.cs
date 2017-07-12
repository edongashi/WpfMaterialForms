using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public abstract class FieldValidator : ValidationRule
    {
        protected FieldValidator(IErrorStringProvider errorProvider, IBoolProxy isEnforced,
            IValueConverter valueConverter, ValidationStep validationStep, bool validatesOnTargetUpdated)
            : base(validationStep, validatesOnTargetUpdated)
        {
            ErrorProvider = errorProvider;
            ValueConverter = valueConverter;
            IsEnforced = isEnforced;
        }

        public IValueConverter ValueConverter { get; }

        public IErrorStringProvider ErrorProvider { get; }

        public IBoolProxy IsEnforced { get; }

        public sealed override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!IsEnforced.Value)
            {
                return new ValidationResult(true, null);
            }

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
