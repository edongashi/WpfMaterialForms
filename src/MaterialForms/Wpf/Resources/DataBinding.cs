using System;
using System.Collections.Generic;
using System.Windows.Data;
using MaterialForms.Wpf.Validation;

namespace MaterialForms.Wpf.Resources
{
    public sealed class DataBinding : Resource
    {
        public DataBinding(string propertyPath, BindingOptions bindingOptions)
            : this(propertyPath, bindingOptions, null, null)
        {
        }

        public DataBinding(string propertyPath, BindingOptions bindingOptions, List<IValidatorProvider> validationRules)
            : this(propertyPath, bindingOptions, validationRules, null)
        {
        }

        public DataBinding(string propertyPath, BindingOptions bindingOptions, List<IValidatorProvider> validationRules, string valueConverter)
            : base(valueConverter)
        {
            PropertyPath = propertyPath;
            BindingOptions = bindingOptions ?? throw new ArgumentNullException(nameof(bindingOptions));
            ValidationRules = validationRules ?? new List<IValidatorProvider>();
        }

        public string PropertyPath { get; }

        public BindingOptions BindingOptions { get; }

        public List<IValidatorProvider> ValidationRules { get; }

        public override bool IsDynamic => true;

        public override BindingBase ProvideBinding(IResourceContext context)
        {
            var binding = context.CreateModelBinding(PropertyPath);
            binding.Converter = GetValueConverter(context);
            BindingOptions.Apply(binding);
            foreach (var validatorProvider in ValidationRules)
            {
                binding.ValidationRules.Add(validatorProvider.GetValidator(context));
            }

            return binding;
        }

        public override bool Equals(Resource other)
        {
            return ReferenceEquals(this, other);
        }

        public override int GetHashCode()
        {
            return PropertyPath.GetHashCode();
        }
    }
}
