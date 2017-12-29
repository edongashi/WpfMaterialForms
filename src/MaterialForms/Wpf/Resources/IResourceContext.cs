using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    /// <summary>
    /// Bridges form elements with the control that contains them.
    /// </summary>
    public interface IResourceContext
    {
        /// <summary>
        /// Gets current model instance.
        /// </summary>
        object GetModelInstance();

        /// <summary>
        /// Gets current context instance.
        /// </summary>
        object GetContextInstance();

        /// <summary>
        /// Creates a binding to the raw model.
        /// </summary>
        /// <returns></returns>
        Binding CreateDirectModelBinding();

        /// <summary>
        /// Creates a new binding to the form model object.
        /// </summary>
        /// <param name="path">Object property path.</param>
        Binding CreateModelBinding(string path);

        /// <summary>
        /// Creates a new binding to the form context object.
        /// </summary>
        /// <param name="path">Object property path.</param>
        Binding CreateContextBinding(string path);

        /// <summary>
        /// Tries to locate a resource from the control's resources.
        /// </summary>
        /// <param name="key">Resource key.</param>
        object TryFindResource(object key);

        /// <summary>
        /// Locates a resource from the control's resources.
        /// </summary>
        /// <param name="key">Resource key.</param>
        object FindResource(object key);

        /// <summary>
        /// Adds a resource to the control's resources.
        /// </summary>
        /// <param name="key">Resource key.</param>
        /// <param name="value">Resource value.</param>
        void AddResource(object key, object value);

        /// <summary>
        /// Notifies that an action has occurred.
        /// </summary>
        void OnAction(object model, string action, object parameter);
    }

    public interface IFrameworkResourceContext : IResourceContext
    {
        FrameworkElement GetOwningElement();
    }
}
