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
            BindingMode = oneTimeBinding ? BindingMode.OneTime : BindingMode.OneWay;
        }

        public PropertyBinding(string propertyPath, BindingMode bindingMode)
            : this(propertyPath, bindingMode, null)
        {
        }

        public PropertyBinding(string propertyPath, BindingMode bindingMode, string valueConverter)
            : base(valueConverter)
        {
            PropertyPath = propertyPath;
            BindingMode = bindingMode;
        }

        public string PropertyPath { get; }

        public BindingMode BindingMode { get; }

        public bool OneTimeBinding => BindingMode == BindingMode.OneTime;

        public override bool IsDynamic => BindingMode != BindingMode.OneTime;

        public override BindingBase ProvideBinding(FrameworkElement container)
        {
            var path = FormatPath(PropertyPath);
            return new Binding(nameof(Controls.MaterialForm.Value) + path)
            {
                Source = container,
                Converter = GetValueConverter(container),
                Mode = BindingMode
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
