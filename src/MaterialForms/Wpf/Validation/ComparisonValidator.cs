using System;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public abstract class ComparisonValidator : FieldValidator
    {
        protected ComparisonValidator(IProxy argument, IStringProxy errorProvider)
            : this(argument, errorProvider, null)
        {
        }

        protected ComparisonValidator(IProxy argument, IStringProxy errorProvider, IValueConverter valueConverter)
            : base(errorProvider, valueConverter)
        {
            Argument = argument ?? throw new ArgumentNullException(nameof(argument));
        }

        public IProxy Argument { get; }
    }
}
