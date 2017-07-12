using System.Collections.Generic;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields.Implementations
{
    public class StringField : DataFormField
    {
        public StringField(string key) : base(key)
        {
        }

        public IValueProvider IsMultiline { get; set; }

        protected internal override void Freeze()
        {
            base.Freeze();
            Resources.Add(nameof(IsMultiline), IsMultiline ?? LiteralValue.False);
        }

        protected internal override IBindingProvider CreateBindingProvider(IResourceContext context, IDictionary<string, IValueProvider> formResources)
        {
            return new StringValue(context, Resources, formResources);
        }

        public override object GetDefaultValue()
        {
            return "";
        }
    }

    public class StringValue : BindingProvider
    {
        public StringValue(IResourceContext context,
            IDictionary<string, IValueProvider> fieldResources,
            IDictionary<string, IValueProvider> formResources)
            : base(context, fieldResources, formResources, true)
        {
        }
    }
}
