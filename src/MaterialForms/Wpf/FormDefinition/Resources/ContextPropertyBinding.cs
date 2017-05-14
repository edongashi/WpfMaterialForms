using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public class ContextPropertyBinding : Resource
    {
        public ContextPropertyBinding(string propertyPath, bool oneTimeBinding)
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
            return new Binding(path)
            {
                Source = element,
                Mode = OneTimeBinding ? BindingMode.OneTime : BindingMode.OneWay
            };
        }
    }
}