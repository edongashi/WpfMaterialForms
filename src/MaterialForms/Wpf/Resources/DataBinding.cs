using System.Collections.Generic;
using System.Windows.Data;
using MaterialForms.Wpf.Validation;

namespace MaterialForms.Wpf.Resources
{
    public sealed class DataBinding : Resource
    {
        public DataBinding(string propertyPath, BindingMode bindingMode)
            : this(propertyPath, bindingMode, null, null)
        {
        }

        public DataBinding(string propertyPath, BindingMode bindingMode, List<IValidatorProvider> validationRules)
            : this(propertyPath, bindingMode, validationRules, null)
        {
        }

        public DataBinding(string propertyPath, BindingMode bindingMode, List<IValidatorProvider> validationRules, string valueConverter)
            : base(valueConverter)
        {
            PropertyPath = propertyPath;
            BindingMode = bindingMode;
            ValidationRules = validationRules ?? new List<IValidatorProvider>();
        }

        public string PropertyPath { get; }

        public BindingMode BindingMode { get; }

        public List<IValidatorProvider> ValidationRules { get; }

        public override bool IsDynamic => true;

        public override BindingBase ProvideBinding(IResourceContext context)
        {
            var binding = context.CreateModelBinding(PropertyPath);
            binding.Converter = GetValueConverter(context);
            binding.Mode = BindingMode;
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
