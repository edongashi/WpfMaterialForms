using System.Collections.Generic;
using System.Windows;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields
{
    public abstract class FormElement
    {
        protected static LiteralValue NullValue = new LiteralValue(null);

        protected static LiteralValue TrueValue = new LiteralValue(true);

        protected static LiteralValue FalseValue = new LiteralValue(false);

        protected FormElement()
        {
            Resources = new Dictionary<string, IValueProvider>();
        }

        public IDictionary<string, IValueProvider> Resources { get; set; }

        protected internal abstract void Freeze();

        protected internal abstract IFieldValueProvider CreateValueProvider(
            FrameworkElement form,
            IDictionary<string, IValueProvider> formResources);
    }
}
