using System.Collections.Generic;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public class DateTimeField : DataFormField
    {
        public DateTimeField(string key) : base(key)
        {
        }

        protected internal override void Freeze()
        {
            base.Freeze();
        }

        protected internal override IBindingProvider CreateBindingProvider(IResourceContext context, IDictionary<string, IValueProvider> formResources)
        {
            return new DateTimeBindingProvider(context, Resources, formResources);
        }
    }

    public class DateTimeBindingProvider : BindingProvider
    {
        public DateTimeBindingProvider(IResourceContext context,
            IDictionary<string, IValueProvider> fieldResources,
            IDictionary<string, IValueProvider> formResources)
            : base(context, fieldResources, formResources, true)
        {
        }
    }
}
