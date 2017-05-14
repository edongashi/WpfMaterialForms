using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public class PropertyBinding : Resource
    {
        public PropertyBinding(string propertyPath, bool oneTimeBinding)
        {
            PropertyPath = propertyPath;
            OneTimeBinding = oneTimeBinding;
        }

        public string PropertyPath { get; }

        public bool OneTimeBinding { get; }

        public override bool IsDynamic => !OneTimeBinding;

        public override BindingBase GetBinding(FrameworkElement element)
        {
            var path = string.IsNullOrEmpty(PropertyPath) ? "." + PropertyPath : null;
            return new Binding(nameof(MaterialForm.Value) + path)
            {
                Source = element,
                Mode = OneTimeBinding ? BindingMode.OneTime : BindingMode.OneWay
            };
        }

        public override bool Equals(Resource other)
        {
            if (other is PropertyBinding resource)
            {
                return PropertyPath == resource.PropertyPath && OneTimeBinding == resource.OneTimeBinding;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return PropertyPath.GetHashCode() ^ (OneTimeBinding ? 123456789 : 741852963);
        }
    }
}