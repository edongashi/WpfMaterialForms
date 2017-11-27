using System.Collections.Generic;
using System.Windows;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public class TextElement : ContentElement
    {
        protected internal override IBindingProvider CreateBindingProvider(IResourceContext context,
            IDictionary<string, IValueProvider> formResources)
        {
            return new TextPresenter(context, Resources, formResources);
        }
    }

    public class TextPresenter : BindingProvider
    {
        static TextPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextPresenter),
                new FrameworkPropertyMetadata(typeof(TextPresenter)));
        }

        public TextPresenter(IResourceContext context, IDictionary<string, IValueProvider> fieldResources,
            IDictionary<string, IValueProvider> formResources)
            : base(context, fieldResources, formResources, true)
        {
        }
    }
}