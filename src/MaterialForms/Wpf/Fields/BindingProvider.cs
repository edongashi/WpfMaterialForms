using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields
{
    /// <summary>
    /// Default implementation of <see cref="IBindingProvider"/>.
    /// </summary>
    public class BindingProvider : IBindingProvider, INotifyPropertyChanged
    {
        private readonly Dictionary<string, BindingProxy> proxyCache;

        public BindingProvider(IResourceContext context,
            IDictionary<string, IValueProvider> fieldResources,
            IDictionary<string, IValueProvider> formResources,
            bool throwOnNotFound)
        {
            Context = context;
            FieldResources = fieldResources;
            FormResources = formResources;
            ThrowOnNotFound = throwOnNotFound;
            proxyCache = new Dictionary<string, BindingProxy>();
        }

        /// <summary>
        /// Gets the context associated with the form control.
        /// </summary>
        public IResourceContext Context { get; }

        /// <summary>
        /// Gets the field resources identified by name.
        /// </summary>
        public IDictionary<string, IValueProvider> FieldResources { get; }

        /// <summary>
        /// Gets the form resources identified by name.
        /// </summary>
        public IDictionary<string, IValueProvider> FormResources { get; }

        /// <summary>
        /// Gets whether this object will throw when a resource is not found.
        /// </summary>
        public bool ThrowOnNotFound { get; }

        /// <summary>
        /// Returns a <see cref="BindingProxy"/> bound to the value returned by <see cref="ProvideValue"/>.
        /// </summary>
        /// <param name="name">Resource name. This is not the object property name.</param>
        /// <returns></returns>
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
                }
                else
                {
                    proxy.Value = value;
                }

                return proxy;
            }
        }

        /// <summary>
        /// Resolves the value for the specified resource.
        /// The result may be a <see cref="BindingBase"/> or a literal value.
        /// </summary>
        /// <param name="name">Resource name. This is not the object property name.</param>
        public virtual object ProvideValue(string name)
        {
            if (FieldResources.TryGetValue(name, out var resource))
            {
                return resource.ProvideValue(Context);
            }

            if (FormResources.TryGetValue(name, out resource))
            {
                return resource.ProvideValue(Context);
            }

            if (ThrowOnNotFound)
            {
                throw new InvalidOperationException($"Resource {name} not found.");
            }

            return null;
        }

        /// <summary>
        /// This event will never fire from the base class.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
