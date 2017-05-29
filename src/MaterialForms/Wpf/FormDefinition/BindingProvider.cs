using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf
{
    /// <summary>
    /// Default implementation of <see cref="IBindingProvider"/>.
    /// </summary>
    public class BindingProvider : IBindingProvider, INotifyPropertyChanged
    {
        private readonly Dictionary<string, BindingProxy> proxyCache;

        public BindingProvider(FrameworkElement form,
            IDictionary<string, IValueProvider> fieldResources,
            IDictionary<string, IValueProvider> formResources)
        {
            Form = form;
            FieldResources = fieldResources;
            FormResources = formResources;
            proxyCache = new Dictionary<string, BindingProxy>();
        }

        public FrameworkElement Form { get; }

        public IDictionary<string, IValueProvider> FieldResources { get; }

        public IDictionary<string, IValueProvider> FormResources { get; }

        public BindingProxy this[string name]
        {
            get
            {
                if (proxyCache.TryGetValue(name, out var proxy))
                {
                    return proxy;
                }

                proxy = new BindingProxy();
                proxyCache[name] = proxy;
                var value = ProvideValue(name);
                if (value is BindingBase binding)
                {
                    BindingOperations.SetBinding(proxy, BindingProxy.ValueProperty, binding);
                    return proxy;
                }

                proxy.Value = value;
                return proxy;
            }
        }

        public virtual object ProvideValue(string name)
        {
            if (FieldResources.TryGetValue(name, out var resource))
            {
                return resource.ProvideValue(Form);
            }

            if (FormResources.TryGetValue(name, out resource))
            {
                return resource.ProvideValue(Form);
            }

            throw new InvalidOperationException($"Resource {name} not found.");
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
