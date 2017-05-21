using System;
using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public class BindingProxyResource : Resource
    {
        public BindingProxyResource(BindingProxy bindingProxy, bool oneTimeBinding)
            : this(bindingProxy, oneTimeBinding, null)
        {
        }

        public BindingProxyResource(BindingProxy bindingProxy, bool oneTimeBinding, IValueConverter valueConverter)
            : base(valueConverter)
        {
            Proxy = bindingProxy ?? throw new ArgumentNullException(nameof(bindingProxy));
            OneTimeBinding = oneTimeBinding;
        }

        public BindingProxy Proxy { get; }

        public bool OneTimeBinding { get; }

        public override bool IsDynamic => !OneTimeBinding;

        public override BindingBase GetBinding(FrameworkElement element)
        {
            return new Binding
            {
                Source = Proxy,
                Path = new PropertyPath(BindingProxy.ValueProperty),
                Converter = ValueConverter,
                Mode = OneTimeBinding ? BindingMode.OneTime : BindingMode.OneWay
            };
        }

        public override bool Equals(Resource other)
        {
            if (other is BindingProxyResource resource)
            {
                return ReferenceEquals(Proxy, resource.Proxy) && Equals(ValueConverter, resource.ValueConverter);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Proxy.GetHashCode();
        }
    }
}
