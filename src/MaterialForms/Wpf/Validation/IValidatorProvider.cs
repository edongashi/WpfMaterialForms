using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Validation
{
    public interface IValidatorProvider
    {
        FieldValidator GetValidator(IResourceContext context, ValidationPipe pipe);
    }
}
