using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class EmptyValidator : FieldValidator
    {
        public EmptyValidator(StringProxy errorMessage, IValueConverter valueConverter) : base(errorMessage, valueConverter)
        {
        }

        public EmptyValidator(StringProxy errorMessage) : base(errorMessage)
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
