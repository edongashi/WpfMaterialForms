using System;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public abstract class ComparisonValidator : FieldValidator
    {
        protected ComparisonValidator(IProxy argument, IStringProxy errorMessage)
            : this(argument, errorMessage, null)
        {
        }

        protected ComparisonValidator(IProxy argument, IStringProxy errorMessage, IValueConverter valueConverter)
            : base(errorMessage, valueConverter)
        {
            Argument = argument ?? throw new ArgumentNullException(nameof(argument));
        }

        public IProxy Argument { get; }
    }
}
