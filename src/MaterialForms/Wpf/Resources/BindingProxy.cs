using System;
using System.Windows;

namespace MaterialForms.Wpf.Resources
{
    /// <summary>
    /// Encapsulates an object bound to a resource.
    /// </summary>
    public class BindingProxy : Freezable, IProxy
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(object),
                typeof(BindingProxy),
                new UIPropertyMetadata(null));

        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }
    }

    internal struct BindingProxyKey : IEquatable<BindingProxyKey>
    {
        public BindingProxyKey(string key)
        {
            Key = key;
        }

        public string Key { get; }

        public bool Equals(BindingProxyKey other)
        {
            return Key == other.Key;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is BindingProxyKey key && Equals(key);
        }

        public override int GetHashCode()
        {
            return Key != null ? Key.GetHashCode() : 0;
        }
    }
}