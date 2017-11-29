using System.Collections.Generic;
using MaterialForms.Wpf.Controls;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields
{
    /// <summary>
    ///     Represents a form element, which is not necessarily an input field.
    /// </summary>
    public abstract class FormElement
    {
        protected FormElement()
        {
            Resources = new Dictionary<string, IValueProvider>();
        }

        protected internal Position LinePosition { get; set; }

        public IDictionary<string, IValueProvider> Resources { get; set; }

        /// <summary>
        ///     Gets or sets the bool resource that determines whether this element will be visible.
        /// </summary>
        public IValueProvider IsVisible { get; set; }

        protected internal virtual void Freeze()
        {
            Resources.Add(nameof(IsVisible), IsVisible ?? LiteralValue.True);
        }

        protected internal abstract IBindingProvider CreateBindingProvider(
            IResourceContext context,
            IDictionary<string, IValueProvider> formResources);
    }
}