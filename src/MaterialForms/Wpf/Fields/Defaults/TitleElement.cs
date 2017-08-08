using System.Collections.Generic;
using System.Windows;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public class TitleElement : ContentElement
    {
        protected internal override IBindingProvider CreateBindingProvider(IResourceContext context, IDictionary<string, IValueProvider> formResources)
        {
            return new TitlePresenter(context, Resources, formResources);
        }
    }

    public class TitlePresenter : BindingProvider
    {
        static TitlePresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TitlePresenter), new FrameworkPropertyMetadata(typeof(TitlePresenter)));
        }

        public TitlePresenter(IResourceContext context, IDictionary<string, IValueProvider> fieldResources, IDictionary<string, IValueProvider> formResources)
            : base(context, fieldResources, formResources, true)
        {
        }
    }
}
