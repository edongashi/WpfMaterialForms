using System.Windows;

namespace MaterialForms.Wpf.Resources
{
    /// <summary>
    /// Encapsulates a string bound to a resource.
    /// </summary>
    public class StringProxy : Freezable, IStringProxy, IProxy
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(string),
                typeof(StringProxy),
                new UIPropertyMetadata(null));

        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        object IProxy.Value => Value;

        protected override Freezable CreateInstanceCore()
        {
            return new StringProxy();
        }
    }
}