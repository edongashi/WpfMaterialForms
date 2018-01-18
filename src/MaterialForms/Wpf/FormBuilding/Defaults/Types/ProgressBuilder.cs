using System;
using MaterialForms.Wpf.Annotations.Display;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Fields.Defaults;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.FormBuilding.Defaults.Types
{
internal class ProgressBuilder : IFieldBuilder
{
    public FormElement TryBuild(IFormProperty property, Func<string, object> deserializer)
    {
        var attribute = property.GetCustomAttribute<ProgressAttribute>();
        return attribute != null
            ? new ProgressField(property.Name)
            {
                Maximum = Utilities.GetResource<object>(attribute.Maximum, 100d, Deserializers.Double)
            }
            : null;
    }
}
}
