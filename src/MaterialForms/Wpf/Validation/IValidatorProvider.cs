using System.Windows;

namespace MaterialForms.Wpf.Validation
{
    public interface IValidatorProvider
    {
        FieldValidator GetValidator(FrameworkElement container);
    }
}
