using System;
using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public sealed class DeferredStringProxyResource : Resource
    {
        public DeferredStringProxyResource(Func<FrameworkElement, StringProxy> bindingProxyProvider, string propertyPath, bool oneTimeBinding, string valueConverter)
            : base(valueConverter)
        {
            ProxyProvider = bindingProxyProvider ?? throw new ArgumentNullException(nameof(bindingProxyProvider));
            PropertyPath = propertyPath;
            OneTimeBinding = oneTimeBinding;
        }

        public Func<FrameworkElement, StringProxy> ProxyProvider { get; }

        public string PropertyPath { get; }

        public bool OneTimeBinding { get; }

        public override bool IsDynamic => !OneTimeBinding;

        public override BindingBase GetBinding(FrameworkElement element)
        {
            var path = FormatPath(PropertyPath);
            return new Binding(nameof(StringProxy.Value) + path)
            {
                Source = ProxyProvider(element) ?? throw new InvalidOperationException("A binding proxy could not be resolved."),
                Converter = GetValueConverter(element),
                Mode = OneTimeBinding ? BindingMode.OneTime : BindingMode.OneWay
            };
        }

        public override Resource Rewrap(string valueConverter)
        {
            return new DeferredStringProxyResource(ProxyProvider, PropertyPath, OneTimeBinding, valueConverter);
        }

        public override bool Equals(Resource other)
        {
            if (other is DeferredStringProxyResource resource)
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
