using System;
using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public class StaticResource : Resource
    {
        public StaticResource(string resourceKey)
            : this(resourceKey, null)
        {
        }

        public StaticResource(string resourceKey, IValueConverter valueConverter)
            : base(valueConverter)
        {
            ResourceKey = resourceKey ?? throw new ArgumentNullException(nameof(resourceKey));
        }

        public string ResourceKey { get; }

        public override bool IsDynamic => false;

        public override BindingBase GetBinding(FrameworkElement element)
        {
            return new Binding
            {
                Source = element.FindResource(ResourceKey),
                Converter = ValueConverter,
                Mode = BindingMode.OneTime
            };
        }

        public override Resource Rewrap(IValueConverter valueConverter)
        {
            return new StaticResource(ResourceKey, valueConverter);
        }

        public override bool Equals(Resource other)
        {
            if (other is StaticResource resource)
            {
                return ResourceKey == resource.ResourceKey && Equals(ValueConverter, resource.ValueConverter);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ResourceKey.GetHashCode();
        }
    }
}
