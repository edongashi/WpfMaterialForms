using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public class CoercedValueProvider<T> : IValueProvider
    {
        private readonly IValueProvider innerProvider;
        private readonly T defaultValue;

        public CoercedValueProvider(IValueProvider innerProvider, T defaultValue)
        {
            this.innerProvider = innerProvider;
            this.defaultValue = defaultValue;
        }

        public BindingBase ProvideBinding(FrameworkElement container)
        {
            var binding = innerProvider.ProvideBinding(container);
            binding.FallbackValue = defaultValue;
            return binding;
        }

        public object ProvideValue(FrameworkElement container)
        {
            var value = innerProvider.ProvideValue(container);
            if (value is BindingBase binding)
            {
                binding.FallbackValue = defaultValue;
                return binding;
            }

            if (value is T)
            {
                return value;
            }

            return defaultValue;
        }
    }
}
