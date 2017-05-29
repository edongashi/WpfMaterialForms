using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields
{
    public interface IBindingProvider
    {
        BindingProxy this[string name] { get; }

        object ProvideValue(string name);
    }
}
