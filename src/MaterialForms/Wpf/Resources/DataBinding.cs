using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public sealed class DataBinding : Resource
    {
        public DataBinding(string propertyPath, BindingMode bindingMode)
            : this(propertyPath, bindingMode, null, null)
        {
        }

        public DataBinding(string propertyPath, BindingMode bindingMode, List<ValidationRule> validationRules)
            : this(propertyPath, bindingMode, validationRules, null)
        {
        }

        public DataBinding(string propertyPath, BindingMode bindingMode, List<ValidationRule> validationRules, string valueConverter)
            : base(valueConverter)
        {
            PropertyPath = propertyPath;
            BindingMode = bindingMode;
            ValidationRules = validationRules ?? new List<ValidationRule>();
        }

        public string PropertyPath { get; }

        public BindingMode BindingMode { get; }

        public List<ValidationRule> ValidationRules { get; }

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

            foreach (var rule in ValidationRules)
            {
                binding.ValidationRules.Add(rule);
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
