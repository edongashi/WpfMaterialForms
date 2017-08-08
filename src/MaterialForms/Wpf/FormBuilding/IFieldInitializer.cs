using System;
using System.Reflection;
using MaterialForms.Wpf.Fields;

namespace MaterialForms.Wpf.FormBuilding
{
    /// <summary>
    /// Initializes built form fields.
    /// </summary>
    public interface IFieldInitializer
    {
        void Initialize(FormElement element, PropertyInfo property, Func<string, object> deserializer);
    }
}
