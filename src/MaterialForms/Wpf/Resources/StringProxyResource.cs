using System;
using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public sealed class StringProxyResource : Resource
    {
        public StringProxyResource(StringProxy proxy, string propertyPath, bool oneTimeBinding, string valueConverter)
            : base(valueConverter)
        {
            Proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
            PropertyPath = propertyPath;
            OneTimeBinding = oneTimeBinding;
        }

        public StringProxy Proxy { get; }

        public string PropertyPath { get; }

        public bool OneTimeBinding { get; }

        public override bool IsDynamic => !OneTimeBinding;

        public override BindingBase ProvideBinding(FrameworkElement container)
        {
            var path = FormatPath(PropertyPath);
            return new Binding(nameof(StringProxy.Value) + path)
            {
                Source = Proxy,
                Converter = GetValueConverter(container),
                Mode = OneTimeBinding ? BindingMode.OneTime : BindingMode.OneWay
            };
        }

        public override Resource Rewrap(string valueConverter)
        {
            return new StringProxyResource(Proxy, PropertyPath, OneTimeBinding, valueConverter);
        }

        public override bool Equals(Resource other)
        {
            if (other is StringProxyResource resource)
            {
                return ReferenceEquals(Proxy, resource.Proxy) 
                    && PropertyPath == resource.PropertyPath
                    && OneTimeBinding == resource.OneTimeBinding
                    && ValueConverter == resource.ValueConverter;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Proxy.GetHashCode();
        }
    }
}
