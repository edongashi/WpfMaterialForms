using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace MaterialForms.Wpf.Validation
{
    public class NotEmptyValidator : FieldValidator
    {
        public NotEmptyValidator(IErrorStringProvider errorProvider, IValueConverter valueConverter) : base(errorProvider, valueConverter)
        {
        }

        public NotEmptyValidator(IErrorStringProvider errorProvider) : base(errorProvider)
        {
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            switch (value)
            {
                case null:
                    return false;
                case string s:
                    return s.Length != 0;
                case IEnumerable<object> e:
                    return e.Any();
                default:
                    return true;
            }
        }
    }
}
