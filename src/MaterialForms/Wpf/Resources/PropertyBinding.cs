using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public sealed class PropertyBinding : Resource
    {
        public PropertyBinding(string propertyPath, bool oneTimeBinding)
            : this(propertyPath, oneTimeBinding, null)
        {
        }

        public PropertyBinding(string propertyPath, bool oneTimeBinding, string valueConverter)
            : base(valueConverter)
        {
            PropertyPath = propertyPath;
            OneTimeBinding = oneTimeBinding;
        }

        public string PropertyPath { get; }

        public bool OneTimeBinding { get; }

        public override bool IsDynamic => !OneTimeBinding;

        public override BindingBase ProvideBinding(FrameworkElement container)
        {
            var path = FormatPath(PropertyPath);
            return new Binding(nameof(Controls.DynamicForm.Value) + path)
            {
                Source = container,
                Converter = GetValueConverter(container),
                Mode = OneTimeBinding ? BindingMode.OneTime : BindingMode.OneWay
            };
        }

        public override Resource Rewrap(string valueConverter)
        {
            return new PropertyBinding(PropertyPath, OneTimeBinding, valueConverter);
        }

        public override bool Equals(Resource other)
        {
            if (other is PropertyBinding resource)
            {
                return PropertyPath == resource.PropertyPath
                       && OneTimeBinding == resource.OneTimeBinding
                       && ValueConverter == resource.ValueConverter;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return PropertyPath.GetHashCode() ^ (OneTimeBinding ? 123456789 : 741852963);
        }
    }
}
