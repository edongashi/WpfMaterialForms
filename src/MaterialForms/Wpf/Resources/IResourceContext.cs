using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public interface IResourceContext
    {
        Binding CreateModelBinding(string path);

        Binding CreateContextBinding(string path);

        object TryFindResource(object key);

        object FindResource(object key);

        void AddResource(object key, object value);
    }
}
