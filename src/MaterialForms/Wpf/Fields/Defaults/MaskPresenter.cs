using System.Collections.Generic;
using System.Windows;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public class MaskPresenter : ValueBindingProvider
    {
        static MaskPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaskPresenter),
                new FrameworkPropertyMetadata(typeof(MaskPresenter)));
        }

        public MaskPresenter(IResourceContext context,
            IDictionary<string, IValueProvider> fieldResources,
            IDictionary<string, IValueProvider> formResources)
            : base(context, fieldResources, formResources, true)
        {
        }
    }
}