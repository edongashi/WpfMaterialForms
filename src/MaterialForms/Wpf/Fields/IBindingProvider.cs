namespace MaterialForms.Wpf.Fields
{
    public interface IBindingProvider
    {
        object ProvideValue(string path);
    }
}
