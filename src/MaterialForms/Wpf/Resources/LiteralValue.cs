using System;
using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public sealed class LiteralValue : Resource
    {
        public LiteralValue(object value)
            : this(value, null)
        {
        }

        public LiteralValue(object value, string valueConverter)
            : base(valueConverter)
        {
            if (value is BindingBase)
            {
                throw new ArgumentException("Value cannot be an instance of BindingBase.", nameof(value));
            }

            Value = value;
        }

        public object Value { get; }

        public override bool IsDynamic => false;

        public override BindingBase ProvideBinding(FrameworkElement container)
        {
            return new Binding
            {
                Source = Value,
                Converter = GetValueConverter(container),
                Mode = BindingMode.OneTime
            };
        }

        public override object ProvideValue(FrameworkElement container)
        {
            return ValueConverter != null
                ? ProvideBinding(container)
                : Value;
        }

        public override Resource Rewrap(string valueConverter)
        {
            return new LiteralValue(Value, valueConverter);
        }

        public override bool Equals(Resource other)
        {
            if (other is LiteralValue resource)
            {
                return Equals(Value, resource.Value)
                    && ValueConverter == other.ValueConverter;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }
    }
}
