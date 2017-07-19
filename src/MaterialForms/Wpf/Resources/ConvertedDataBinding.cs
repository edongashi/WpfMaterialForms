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
        public ConvertedDataBinding(string propertyPath, BindingOptions bindingOptions,
            List<IValidatorProvider> validationRules, Func<string, CultureInfo, object> deserializer,
            Func<IResourceContext, IErrorStringProvider> conversionErrorStringProvider)
        {
            PropertyPath = propertyPath;
            BindingOptions = bindingOptions ?? throw new ArgumentNullException(nameof(bindingOptions));
            Deserializer = deserializer;
            ValidationRules = validationRules ?? new List<IValidatorProvider>();
            ConversionErrorStringProvider = conversionErrorStringProvider;
        }

        public string PropertyPath { get; }

        public BindingOptions BindingOptions { get; }

        public List<IValidatorProvider> ValidationRules { get; }

        public Func<string, CultureInfo, object> Deserializer { get; }

        public Func<IResourceContext, IErrorStringProvider> ConversionErrorStringProvider { get; }

        public BindingBase ProvideBinding(IResourceContext context)
        {
            var binding = context.CreateModelBinding(PropertyPath);
            BindingOptions.Apply(binding);
            binding.Converter = new StringTypeConverter(Deserializer);
            binding.ValidationRules.Add(new ConversionValidator(Deserializer, ConversionErrorStringProvider(context), binding.ConverterCulture));
            foreach (var validatorProvider in ValidationRules)
            {
                binding.ValidationRules.Add(validatorProvider.GetValidator(context));
            }

            return binding;
        }

        public object ProvideValue(IResourceContext context) => ProvideBinding(context);
    }
}
