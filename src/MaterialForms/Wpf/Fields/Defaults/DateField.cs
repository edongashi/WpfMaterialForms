using System.Collections.Generic;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public class DateField : DataFormField
    {
        public DateField(string key) : base(key, typeof(string))
        {
        }

        protected internal override void Freeze()
        {
            base.Freeze();
        }

        protected internal override IBindingProvider CreateBindingProvider(IResourceContext context, IDictionary<string, IValueProvider> formResources)
        {
            return new DatePresenter(context, Resources, formResources);
        }
    }
}