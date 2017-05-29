using System.Collections.Generic;
using System.Windows;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields
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

        protected internal override IBindingProvider CreateBindingProvider(FrameworkElement form, IDictionary<string, IValueProvider> formResources)
        {
            return new StringValue(form, Resources, formResources);
        }

        public override object GetDefaultValue() => "";
    }

    public class StringValue : BindingProvider
    {
        public StringValue(FrameworkElement form,
            IDictionary<string, IValueProvider> fieldResources,
            IDictionary<string, IValueProvider> formResources)
            : base(form, fieldResources, formResources)
        {
        }
    }
}
