using System.Collections.Generic;
using System.Windows;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public class DatePresenter : ValueBindingProvider
    {
        static DatePresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DatePresenter), new FrameworkPropertyMetadata(typeof(DatePresenter)));
        }

        public DatePresenter(IResourceContext context,
            IDictionary<string, IValueProvider> fieldResources,
            IDictionary<string, IValueProvider> formResources)
            : base(context, fieldResources, formResources, true)
        {
        }
    }
}