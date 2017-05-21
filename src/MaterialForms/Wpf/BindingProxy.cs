using System.Windows;

namespace MaterialForms.Wpf
{
    /// <summary>
    /// Encapsulates an object bound to a resource.
    /// </summary>
    public class BindingProxy : Freezable
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(object),
                typeof(BindingProxy),
                new UIPropertyMetadata(null));

        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }
    }
}