using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields
{
    public class StringField : DataFormField
    {
        public StringField(string name) : base(name, BindingMode.Default)
        {
        }

        protected override IFieldValueProvider CreateValueProvider(FrameworkElement form, IDictionary<string, IValueProvider> formResources)
        {
            return new StringValue(form, Resources, formResources);
        }
    }

    public class StringValue : FieldValueProvider
    {
        public StringValue(FrameworkElement form,
            IDictionary<string, IValueProvider> fieldResources,
            IDictionary<string, IValueProvider> formResources)
            : base(form, fieldResources, formResources)
        {
        }
    }
}
