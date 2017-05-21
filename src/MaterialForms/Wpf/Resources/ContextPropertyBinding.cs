using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public sealed class ContextPropertyBinding : Resource
    {
        public ContextPropertyBinding(string propertyPath, bool oneTimeBinding)
            : this(propertyPath, oneTimeBinding, null)
        {
        }

        public ContextPropertyBinding(string propertyPath, bool oneTimeBinding, IValueConverter valueConverter)
            : base(valueConverter)
        {
            PropertyPath = propertyPath;
            OneTimeBinding = oneTimeBinding;
        }

        public string PropertyPath { get; }

        public bool OneTimeBinding { get; }

        public override bool IsDynamic => !OneTimeBinding;

        public override BindingBase GetBinding(FrameworkElement element)
        {
            var path = FormatPath(PropertyPath);
            return new Binding(nameof(Controls.MaterialForm.Context) + path)
            {
                Source = element,
                Converter = ValueConverter,
                Mode = OneTimeBinding ? BindingMode.OneTime : BindingMode.OneWay
            };
        }

        public override Resource Rewrap(IValueConverter valueConverter)
        {
            return new ContextPropertyBinding(PropertyPath, OneTimeBinding, valueConverter);
        }

        public override bool Equals(Resource other)
        {
            if (other is ContextPropertyBinding resource)
            {
                return PropertyPath == resource.PropertyPath && OneTimeBinding == resource.OneTimeBinding &&
                       Equals(ValueConverter, resource.ValueConverter);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return PropertyPath.GetHashCode() ^ (OneTimeBinding ? 741852963 : 123456789);
        }
    }
}
