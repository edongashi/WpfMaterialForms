using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public interface IValueProvider
    {
        BindingBase ProvideBinding(FrameworkElement container);

        object ProvideValue(FrameworkElement container);
    }

    public static class ValueProviderExtensions
    {
        public static BindingProxy GetValue(this IValueProvider valueProvider, FrameworkElement container)
        {
            var proxy = new BindingProxy();
            var value = valueProvider.ProvideValue(container);
            if (value is BindingBase binding)
            {
                BindingOperations.SetBinding(proxy, BindingProxy.ValueProperty, binding);
            }
            else
            {
                proxy.Value = value;
            }

            return proxy;
        }

        public static StringProxy GetStringValue(this IValueProvider valueProvider, FrameworkElement container)
        {
            var proxy = new StringProxy();
            var value = valueProvider.ProvideValue(container);
            if (value is BindingBase binding)
            {
                BindingOperations.SetBinding(proxy, StringProxy.ValueProperty, binding);
            }
            else
            {
                proxy.Value = value?.ToString();
            }

            return proxy;
        }
    }
}
