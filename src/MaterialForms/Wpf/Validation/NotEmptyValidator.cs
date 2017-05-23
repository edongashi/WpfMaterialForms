using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class NotEmptyValidator : FieldValidator
    {
        public NotEmptyValidator(IStringProxy errorMessage, IValueConverter valueConverter) : base(errorMessage, valueConverter)
        {
        }

        public NotEmptyValidator(IStringProxy errorMessage) : base(errorMessage)
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
