using System;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public abstract class ComparisonValidator : FieldValidator
    {
        protected ComparisonValidator(BindingProxy argument, StringProxy errorMessage)
            : this(argument, errorMessage, null)
        {
        }

        protected ComparisonValidator(BindingProxy argument, StringProxy errorMessage, IValueConverter valueConverter)
            : base(errorMessage, valueConverter)
        {
            Argument = argument ?? throw new ArgumentNullException(nameof(argument));
        }

        public BindingProxy Argument { get; }
    }
}
