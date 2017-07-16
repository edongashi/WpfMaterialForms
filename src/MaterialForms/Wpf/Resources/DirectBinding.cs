using System.Collections.Generic;
using System.Windows.Data;
using MaterialForms.Wpf.Validation;

namespace MaterialForms.Wpf.Resources
{
    public sealed class DirectBinding : Resource
    {
        public DirectBinding(BindingMode bindingMode)
            : this(bindingMode, null, null)
        {
        }

        public DirectBinding(BindingMode bindingMode, List<IValidatorProvider> validationRules)
            : this(bindingMode, validationRules, null)
        {
        }

        public DirectBinding(BindingMode bindingMode, List<IValidatorProvider> validationRules, string valueConverter)
            : base(valueConverter)
        {
            BindingMode = bindingMode;
            ValidationRules = validationRules ?? new List<IValidatorProvider>();
        }

        public BindingMode BindingMode { get; }

        public List<IValidatorProvider> ValidationRules { get; }

        public override bool IsDynamic => true;

        public override BindingBase ProvideBinding(IResourceContext context)
        {
            var binding = context.CreateDirectModelBinding();
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
            return BindingMode.GetHashCode();
        }
    }
}
