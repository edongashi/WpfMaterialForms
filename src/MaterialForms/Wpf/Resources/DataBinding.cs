using System.Collections.Generic;
using System.Windows;
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

        public override BindingBase ProvideBinding(FrameworkElement container)
        {
            var path = FormatPath(PropertyPath);
            var binding =  new Binding(nameof(Controls.MaterialForm.Value) + path)
            {
                Source = container,
                Converter = GetValueConverter(container),
                Mode = BindingMode
            };

            foreach (var validatorProvider in ValidationRules)
            {
                binding.ValidationRules.Add(validatorProvider.GetValidator(container));
            }

            return binding;
        }

        public override Resource Rewrap(string valueConverter)
        {
            return new DataBinding(PropertyPath, BindingMode, ValidationRules, valueConverter);
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
