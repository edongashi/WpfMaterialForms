using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace MaterialForms.Wpf.Resources
{
    public class DynamicResource : Resource
    {
        public DynamicResource(string resourceKey)
        {
            ResourceKey = resourceKey ?? throw new ArgumentNullException(nameof(resourceKey));
        }

        public string ResourceKey { get; }

        public override bool IsDynamic => true;

        public override BindingBase GetBinding(FrameworkElement element)
        {
            var key = new DynamicResourceKey(ResourceKey);
            if (element.Resources.Contains(key))
            {
                return CreateBinding((BindingProxy)element.Resources[key]);
            }

            var proxy = new BindingProxy();
            element.Resources.Add(key, proxy);
            proxy.Data = new DynamicResourceExtension(ResourceKey).ProvideValue(new Target(element));
            return CreateBinding(proxy);
        }

        private Binding CreateBinding(BindingProxy proxy)
        {
            return new Binding
            {
                Path = new PropertyPath(BindingProxy.DataProperty),
                Source = proxy,
                Mode = BindingMode.OneWay
            };
        }

        private struct Target : IServiceProvider, IProvideValueTarget
        {
            public Target(object targetObject)
            {
                TargetObject = targetObject;
            }

            public object GetService(Type serviceType)
            {
                if (serviceType == typeof(IProvideValueTarget))
                {
                    return this;
                }

                return null;
            }

            public object TargetObject { get; }

            public object TargetProperty => null;
        }
    }

    internal struct DynamicResourceKey : IEquatable<DynamicResourceKey>
    {
        public DynamicResourceKey(string key)
        {
            Key = key;
        }

        public string Key { get; }

        public bool Equals(DynamicResourceKey other)
        {
            return string.Equals(Key, other.Key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is DynamicResourceKey && Equals((DynamicResourceKey)obj);
        }

        public override int GetHashCode()
        {
            return (Key != null ? Key.GetHashCode() : 0);
        }
    }
}
