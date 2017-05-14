using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public class LiteralValue : Resource
    {
        public LiteralValue(object value)
        {
            Value = value;
        }

        public object Value { get; }

        public override bool IsDynamic => false;

        public override BindingBase GetBinding(FrameworkElement element)
        {
            return new Binding
            {
                Source = Value,
                Mode = BindingMode.OneTime
            };
        }

        public override bool Equals(Resource other)
        {
            if (other is LiteralValue resource)
            {
                return Equals(Value, resource.Value);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }
    }
}