using System;
using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public sealed class StaticResource : Resource
    {
        public StaticResource(string resourceKey)
            : this(resourceKey, null)
        {
        }

        public StaticResource(string resourceKey, string valueConverter)
            : base(valueConverter)
        {
            ResourceKey = resourceKey ?? throw new ArgumentNullException(nameof(resourceKey));
        }

        public string ResourceKey { get; }

        public override bool IsDynamic => false;

        public override BindingBase ProvideBinding(FrameworkElement container)
        {
            return new Binding
            {
                Source = container.FindResource(ResourceKey),
                Converter = GetValueConverter(container),
                Mode = BindingMode.OneTime
            };
        }

        public override Resource Rewrap(string valueConverter)
        {
            return new StaticResource(ResourceKey, valueConverter);
        }

        public override bool Equals(Resource other)
        {
            if (other is StaticResource resource)
            {
                return ResourceKey == resource.ResourceKey
                    && ValueConverter == resource.ValueConverter;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ResourceKey.GetHashCode();
        }
    }
}
