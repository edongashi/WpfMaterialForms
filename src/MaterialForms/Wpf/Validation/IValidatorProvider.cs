using System.Windows;
using System.Windows.Controls;

namespace MaterialForms.Wpf.Validation
{
    public interface IValidatorProvider
    {
        ValidationRule GetValidator(FrameworkElement container);
    }
}
