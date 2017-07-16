using System;
using System.Reflection;
using Humanizer;
using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.FormBuilding.Defaults.Initializers
{
    public class FieldInitializer : IFieldInitializer
    {
        public void Initialize(FormElement element, PropertyInfo property, Func<string, object> deserializer)
        {
            var attr = property.GetCustomAttribute<FieldAttribute>();
            if (attr == null)
            {
                return;
            }

            if (!(element is FormField field))
            {
                return;
            }

            field.Name = attr.HasName
                ? TypeUtilities.GetStringResource(attr.Name)
                : new LiteralValue(property.Name.Humanize());

            field.ToolTip = TypeUtilities.GetStringResource(attr.ToolTip);

            field.Icon = TypeUtilities.GetResource<PackIconKind>(attr.Icon, null, Deserializers.Enum<PackIconKind>());

            if (property.CanWrite && property.GetSetMethod(true).IsPublic)
            {
                field.IsReadOnly = TypeUtilities.GetResource<bool>(attr.IsReadOnly, false, Deserializers.Boolean);
            }
            else
            {
                field.IsReadOnly = new LiteralValue(true);
            }

            field.DefaultValue = TypeUtilities.GetResource<object>(attr.DefaultValue, null, deserializer);
        }
    }
}
