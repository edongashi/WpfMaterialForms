using System;
using MaterialForms.Wpf.Fields;

namespace MaterialForms.Wpf.FormBuilding
{
    /// <summary>
    /// Intercepts properties and builds form elements if conditions are met.
    /// </summary>
    public interface IFieldBuilder
    {
        FormElement TryBuild(IFormProperty property, Func<string, object> deserializer);
    }
}
