using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace MaterialForms.Wpf.Validation
{
    public class EmptyValidator : FieldValidator
    {
        public EmptyValidator(IErrorStringProvider errorProvider, IValueConverter valueConverter) : base(errorProvider, valueConverter)
        {
        }

        public EmptyValidator(IErrorStringProvider errorProvider) : base(errorProvider)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            switch (value)
            {
                case null:
                    return true;
                case string s:
                    return s.Length == 0;
                case IEnumerable<object> e:
                    return !e.Any();
                default:
                    return true;
            }
        }
    }
}
