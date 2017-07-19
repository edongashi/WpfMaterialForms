using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Validation;

namespace MaterialForms.Wpf.Resources
{
    public sealed class ConvertedDataBinding : IValueProvider
    {
        public ConvertedDataBinding(string propertyPath, BindingMode bindingMode,
            List<IValidatorProvider> validationRules, Func<string, CultureInfo, object> deserializer,
            Func<IResourceContext, IErrorStringProvider> conversionErrorStringProvider)
        {
            PropertyPath = propertyPath;
            BindingMode = bindingMode;
            Deserializer = deserializer;
            ValidationRules = validationRules ?? new List<IValidatorProvider>();
            ConversionErrorStringProvider = conversionErrorStringProvider;
        }

        public string PropertyPath { get; }

        public BindingMode BindingMode { get; }

        public List<IValidatorProvider> ValidationRules { get; }

        public Func<string, CultureInfo, object> Deserializer { get; }

        public Func<IResourceContext, IErrorStringProvider> ConversionErrorStringProvider { get; }

        public BindingBase ProvideBinding(IResourceContext context)
        {
            var binding = context.CreateModelBinding(PropertyPath);
            binding.Mode = BindingMode;
            binding.Converter = new StringTypeConverter(Deserializer);
            binding.ValidationRules.Add(new ConversionValidator(Deserializer, ConversionErrorStringProvider(context)));
            foreach (var validatorProvider in ValidationRules)
            {
                binding.ValidationRules.Add(validatorProvider.GetValidator(context));
            }

            return binding;
        }

        public object ProvideValue(IResourceContext context) => ProvideBinding(context);
    }
}
