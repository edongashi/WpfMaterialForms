using System;
using System.Collections.Generic;
using System.Windows;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public class DateTimeField : DataFormField
    {
        public DateTimeField(string key, bool nullable) : base(key, nullable ? typeof(DateTime?) : typeof(DateTime))
        {
        }

        protected internal override IBindingProvider CreateBindingProvider(IResourceContext context, IDictionary<string, IValueProvider> formResources)
        {
            return new DateTimePresenter(context, Resources, formResources);
        }
    }

    public class DateTimePresenter : ValueBindingProvider
    {
        static DateTimePresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DateTimePresenter), new FrameworkPropertyMetadata(typeof(DateTimePresenter)));
        }

        public DateTimePresenter(IResourceContext context,
            IDictionary<string, IValueProvider> fieldResources,
            IDictionary<string, IValueProvider> formResources)
            : base(context, fieldResources, formResources, true)
        {
        }
    }
}
