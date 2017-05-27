using System.Collections.Generic;
using System.Windows;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields
{
    public abstract class FormElement
    {
        protected FormElement()
        {
            Resources = new Dictionary<string, IValueProvider>();
        }

        public IDictionary<string, IValueProvider> Resources { get; set; }

        protected internal abstract void Freeze();

        protected internal abstract IBindingProvider CreateValueProvider(
            FrameworkElement form,
            IDictionary<string, IValueProvider> formResources);
    }
}
