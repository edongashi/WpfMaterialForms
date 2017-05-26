using System.Windows;

namespace MaterialForms.Wpf.Resources
{
    /// <summary>
    /// Encapsulates a string bound to a resource.
    /// </summary>
    public class BoolProxy : Freezable, IBoolProxy, IProxy
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(bool),
                typeof(BoolProxy),
                new UIPropertyMetadata(false));

        public bool Value
        {
            get => (bool)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        object IProxy.Value => Value;

        protected override Freezable CreateInstanceCore()
        {
            return new BoolProxy();
        }
    }
}