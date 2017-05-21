using System;
using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public sealed class BindingProxyResource : Resource
    {
        public BindingProxyResource(BindingProxy bindingProxy, string propertyPath, bool oneTimeBinding, IValueConverter valueConverter)
            : base(valueConverter)
        {
            Proxy = bindingProxy ?? throw new ArgumentNullException(nameof(bindingProxy));
            PropertyPath = propertyPath;
            OneTimeBinding = oneTimeBinding;
        }

        public BindingProxy Proxy { get; }

        public string PropertyPath { get; }

        public bool OneTimeBinding { get; }

        public override bool IsDynamic => !OneTimeBinding;

        public override BindingBase GetBinding(FrameworkElement element)
        {
            var path = FormatPath(PropertyPath);
            return new Binding(nameof(BindingProxy.Value) + path)
            {
                Source = Proxy,
                Converter = ValueConverter,
                Mode = OneTimeBinding ? BindingMode.OneTime : BindingMode.OneWay
            };
        }

        public override Resource Rewrap(IValueConverter valueConverter)
        {
            return new BindingProxyResource(Proxy, PropertyPath, OneTimeBinding, valueConverter);
        }

        public override bool Equals(Resource other)
        {
            if (other is BindingProxyResource resource)
            {
                return ReferenceEquals(Proxy, resource.Proxy)
                       && PropertyPath == resource.PropertyPath
                       && OneTimeBinding == resource.OneTimeBinding
                       && Equals(ValueConverter, resource.ValueConverter);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Proxy.GetHashCode();
        }
    }
}
