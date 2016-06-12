using System.Globalization;
using System.Windows.Controls;

namespace MaterialForms.Validation
{
    internal class BindableValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var rule = Provider?.BoundRule;
            return rule == null ? ValidationResult.ValidResult : rule.Validate(value, cultureInfo);
        }

        public ValidationRuleProvider Provider { get; set; }
    }
}
