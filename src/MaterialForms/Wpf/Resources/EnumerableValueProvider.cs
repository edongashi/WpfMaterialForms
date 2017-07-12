using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public class EnumerableStringValueProvider : IValueProvider
    {
        private readonly IEnumerable<KeyValuePair<ValueType, IValueProvider>> elements;

        public EnumerableStringValueProvider(IEnumerable<KeyValuePair<ValueType, IValueProvider>> elements)
        {
            this.elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        public BindingBase ProvideBinding(IResourceContext context)
        {
            return new Binding
            {
                Source = ProvideValue(context),
                Mode = BindingMode.OneTime
            };
        }

        public object ProvideValue(IResourceContext context)
        {
            return elements.Select(e =>
            {
                var proxy = e.Value.GetStringValue(context);
                proxy.Key = e.Key;
                return proxy;
            }).ToList();
        }
    }
}
