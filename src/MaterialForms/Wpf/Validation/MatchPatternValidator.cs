using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class MatchPatternValidator : ComparisonValidator
    {
        public MatchPatternValidator(IProxy argument, IErrorStringProvider errorProvider, IBoolProxy isEnforced, IValueConverter valueConverter) : base(argument, errorProvider, isEnforced, valueConverter)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            if (!(Argument.Value is string pattern))
            {
                return true;
            }

            if (value is string s)
            {
                return Regex.IsMatch(s, pattern);
            }

            return false;
        }
    }
}
