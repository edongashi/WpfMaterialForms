using System.Windows.Controls;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public interface IValidatorProvider
    {
        ValidationRule GetValidator(IResourceContext context);
    }
}
