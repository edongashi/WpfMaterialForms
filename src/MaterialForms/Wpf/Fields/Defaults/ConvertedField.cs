using System;
using System.Collections.Generic;
using System.Globalization;
using MaterialForms.Wpf.Resources;
using MaterialForms.Wpf.Validation;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public sealed class ConvertedField : DataFormField
    {
        public ConvertedField(string key, Func<string, CultureInfo, object> deserializer) : base(key)
        {
            Deserializer = deserializer;
            CreateBinding = false;
        }

        public Func<string, CultureInfo, object> Deserializer { get; }

        // TODO: replace with provider
        public IErrorStringProvider ConversionErrorMessage { get; set; }

        protected internal override void Freeze()
        {
            base.Freeze();

            Resources.Add("Value",
                new ConvertedDataBinding(Key, BindingMode, Validators, Deserializer,
                    // TODO: This is temporary.
                    context => ConversionErrorMessage ?? new PlainErrorStringProvider("Invalid value.")));
        }

        protected internal override IBindingProvider CreateBindingProvider(IResourceContext context,
            IDictionary<string, IValueProvider> formResources)
        {
            return new ConvertedFieldBindingProvider(context, Resources, formResources);
        }
    }

    public class ConvertedFieldBindingProvider : BindingProvider
    {
        public ConvertedFieldBindingProvider(IResourceContext context,
            IDictionary<string, IValueProvider> fieldResources,
            IDictionary<string, IValueProvider> formResources)
            : base(context, fieldResources, formResources, true)
        {
        }
    }
}
