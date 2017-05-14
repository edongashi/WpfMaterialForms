using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public abstract class Resource
    {
        public abstract bool IsDynamic { get; }

        public abstract BindingBase GetBinding(FrameworkElement element);
    }
}
