using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class NotEmptyValidator : FieldValidator
    {
        public NotEmptyValidator(IErrorStringProvider errorProvider, IBoolProxy isEnforced,
            IValueConverter valueConverter, ValidationStep validationStep, bool validatesOnTargetUpdated)
            : base(errorProvider, isEnforced, valueConverter, validationStep, validatesOnTargetUpdated)
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
