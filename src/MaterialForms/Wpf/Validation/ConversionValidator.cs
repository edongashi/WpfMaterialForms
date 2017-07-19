using System;
using System.Globalization;
using System.Windows.Controls;

namespace MaterialForms.Wpf.Validation
{
    public class ConversionValidator : ValidationRule
    {
        private readonly Func<string, CultureInfo, object> deserializer;
        private readonly IErrorStringProvider errorProvider;

        public ConversionValidator(Func<string, CultureInfo, object> deserializer, IErrorStringProvider errorProvider)
            : base(ValidationStep.RawProposedValue, false)
        {
            this.deserializer = deserializer;
            this.errorProvider = errorProvider;
        }

        public sealed override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                deserializer(value as string, cultureInfo);
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, errorProvider.GetErrorMessage(value));
            }
        }
    }
}
