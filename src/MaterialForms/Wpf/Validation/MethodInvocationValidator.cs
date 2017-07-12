using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public class MethodInvocationValidator : FieldValidator
    {
        private readonly Func<object, CultureInfo, bool> method;

        public MethodInvocationValidator(Func<object, CultureInfo, bool> method, IErrorStringProvider errorProvider,
            IBoolProxy isEnforced, IValueConverter valueConverter, ValidationStep validationStep, bool validatesOnTargetUpdated)
            : base(errorProvider, isEnforced, valueConverter, validationStep, validatesOnTargetUpdated)
        {
            this.method = method;
        }

        protected override bool ValidateValue(object value, CultureInfo cultureInfo)
        {
            return method(value, cultureInfo);
        }
    }
}
