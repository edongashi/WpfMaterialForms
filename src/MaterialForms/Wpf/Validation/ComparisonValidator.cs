using System;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public abstract class ComparisonValidator : FieldValidator
    {
        protected ComparisonValidator(IProxy argument, IErrorStringProvider errorProvider, IBoolProxy isEnforced,
            IValueConverter valueConverter, ValidationStep validationStep, bool validatesOnTargetUpdated)
            : base(errorProvider, isEnforced, valueConverter, validationStep, validatesOnTargetUpdated)
        {
            Argument = argument ?? throw new ArgumentNullException(nameof(argument));
        }

        public IProxy Argument { get; }
    }
}
