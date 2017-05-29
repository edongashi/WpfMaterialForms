using System.Collections.Generic;
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

        protected internal abstract IBindingProvider CreateBindingProvider(
            IResourceContext context,
            IDictionary<string, IValueProvider> formResources);
    }
}
