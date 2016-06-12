using System.Windows;
using System.Windows.Controls;

namespace MaterialForms.Validation
{
    internal class ValidationRuleProvider : DependencyObject
    {
        public static readonly DependencyProperty BoundRuleProperty =
            DependencyProperty.Register(
                "BoundRule",
                typeof(ValidationRule),
                typeof(ValidationRuleProvider));

        public ValidationRule BoundRule
        {
            get { return (ValidationRule)GetValue(BoundRuleProperty); }
            set { SetValue(BoundRuleProperty, value); }
        }
    }
}
