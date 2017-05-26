using System.Globalization;
using System.Windows;
using System.Windows.Data;
using MaterialForms.Wpf.Resources.ValueConverters;

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

        public static BoolProxy GetBoolValue(this IValueProvider valueProvider, FrameworkElement container)
        {
            var proxy = new BoolProxy();
            var value = valueProvider.ProvideValue(container);
            if (value is BindingBase binding)
            {
                BindingOperations.SetBinding(proxy, BoolProxy.ValueProperty, binding);
            }
            else
            {
                proxy.Value = value is bool b && b;
            }

            return proxy;
        }

        public static IValueProvider Wrap(this IValueProvider valueProvider, string valueConverter)
        {
            return valueConverter == null 
                ? valueProvider 
                : new ValueProviderWrapper(valueProvider, valueConverter);
        }

        private class ValueProviderWrapper : IValueProvider
        {
            private readonly IValueProvider innerProvider;
            private readonly string valueConverter;

            public ValueProviderWrapper(IValueProvider innerProvider, string valueConverter)
            {
                this.innerProvider = innerProvider;
                this.valueConverter = valueConverter;
            }

            public BindingBase ProvideBinding(FrameworkElement container)
            {
                var bindingBase = innerProvider.ProvideBinding(container);
                if (bindingBase is Binding binding)
                {
                    var converter = Resource.GetValueConverter(container, valueConverter);
                    binding.Converter = binding.Converter == null 
                        ? converter 
                        : new ConverterWrapper(converter, binding.Converter);
                }

                return bindingBase;
            }

            public object ProvideValue(FrameworkElement container)
            {
                var value = innerProvider.ProvideValue(container);
                var converter = Resource.GetValueConverter(container, valueConverter);
                if (value is Binding binding)
                {
                    binding.Converter = binding.Converter == null
                        ? converter
                        : new ConverterWrapper(converter, binding.Converter);
                }
                else if (value is BindingBase)
                {
                    return value;
                }

                return converter.Convert(value, typeof(object), null, CultureInfo.CurrentCulture);
            }
        }
    }
}
