using System;
using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public sealed class DeferredBindingProxyResource : Resource
    {
        public DeferredBindingProxyResource(Func<FrameworkElement, BindingProxy> bindingProxyProvider, string propertyPath, bool oneTimeBinding, string valueConverter)
            : base(valueConverter)
        {
            ProxyProvider = bindingProxyProvider ?? throw new ArgumentNullException(nameof(bindingProxyProvider));
            PropertyPath = propertyPath;
            OneTimeBinding = oneTimeBinding;
        }

        public Func<FrameworkElement, BindingProxy> ProxyProvider { get; }

        public string PropertyPath { get; }

        public bool OneTimeBinding { get; }

        public override bool IsDynamic => !OneTimeBinding;

        public override BindingBase ProvideBinding(FrameworkElement container)
        {
            var path = FormatPath(PropertyPath);
            return new Binding(nameof(BindingProxy.Value) + path)
            {
                Source = ProxyProvider(container) ?? throw new InvalidOperationException("A binding proxy could not be resolved."),
                Converter = GetValueConverter(container),
                Mode = OneTimeBinding ? BindingMode.OneTime : BindingMode.OneWay
            };
        }

        public override Resource Rewrap(string valueConverter)
        {
            return new DeferredBindingProxyResource(ProxyProvider, PropertyPath, OneTimeBinding, valueConverter);
        }

        public override bool Equals(Resource other)
        {
            if (other is DeferredBindingProxyResource resource)
            {
                return ReferenceEquals(ProxyProvider, resource.ProxyProvider)
                       && PropertyPath == resource.PropertyPath
                       && OneTimeBinding == resource.OneTimeBinding
                       && ValueConverter == resource.ValueConverter;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ProxyProvider.GetHashCode();
        }
    }
}
