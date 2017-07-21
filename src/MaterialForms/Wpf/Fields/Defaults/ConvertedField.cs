using System;
using System.Collections.Generic;
using System.Globalization;
using MaterialForms.Wpf.Resources;
using MaterialForms.Wpf.Validation;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public class ConvertedField : DataFormField
    {
        public ConvertedField(string key, Type propertyType, Func<string, CultureInfo, object> deserializer) : base(key, propertyType)
        {
            Deserializer = deserializer;
            CreateBinding = false;
        }

        public Func<string, CultureInfo, object> Deserializer { get; }

        public Func<IResourceContext, IErrorStringProvider> ConversionErrorMessage { get; set; }

        protected internal override void Freeze()
        {
            base.Freeze();
            Resources.Add("Value",
                new ConvertedDataBinding(Key, BindingOptions, Validators, Deserializer,
                    ConversionErrorMessage ?? (ctx => new PlainErrorStringProvider("Invalid value."))));
        }

        protected internal override IBindingProvider CreateBindingProvider(IResourceContext context,
            IDictionary<string, IValueProvider> formResources)
        {
            return new ConvertedFieldBindingProvider(context, Resources, formResources);
        }
    }

    public class ConvertedFieldBindingProvider : ValueBindingProvider
    {
        public ConvertedFieldBindingProvider(IResourceContext context,
            IDictionary<string, IValueProvider> fieldResources,
            IDictionary<string, IValueProvider> formResources)
            : base(context, fieldResources, formResources, true)
        {
        }
    }
}
