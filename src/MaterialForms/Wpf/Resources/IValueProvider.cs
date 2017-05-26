using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public interface IValueProvider
    {
        BindingBase ProvideBinding(FrameworkElement container);

        object ProvideValue(FrameworkElement container);
    }
}
