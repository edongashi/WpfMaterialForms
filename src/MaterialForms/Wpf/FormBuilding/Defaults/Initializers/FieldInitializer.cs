using System;
using System.Reflection;
using Humanizer;
using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.FormBuilding.Defaults.Initializers
{
    internal class FieldInitializer : IFieldInitializer
    {
        public void Initialize(FormElement element, PropertyInfo property, Func<string, object> deserializer)
        {
            var attr = property.GetCustomAttribute<FieldAttribute>();
            if (attr == null)
            {
                if (element is FormField formField && formField.Name == null)
                {
                    formField.Name = new LiteralValue(property.Name.Humanize());
                }

                return;
            }

            element.IsVisible = TypeUtilities.GetResource<bool>(attr.IsVisible, true, deserializer);

            if (element is FormField field)
            {
                field.Name = attr.HasName
                    ? TypeUtilities.GetStringResource(attr.Name)
                    : new LiteralValue(property.Name.Humanize());
                field.ToolTip = TypeUtilities.GetStringResource(attr.ToolTip);

                if (attr.Icon is -1 || (attr.Icon is string s && string.Equals(s, "empty", StringComparison.OrdinalIgnoreCase)))
                {
                    field.Icon = new LiteralValue((PackIconKind)(-1));
                }
                else
                {
                    field.Icon = TypeUtilities.GetResource<PackIconKind>(attr.Icon, null, Deserializers.Enum<PackIconKind>());
                }
            }

            if (element is DataFormField dataField)
            {
                if (property.CanWrite && property.GetSetMethod(true).IsPublic)
                {
                    dataField.IsReadOnly = TypeUtilities.GetResource<bool>(attr.IsReadOnly, false, Deserializers.Boolean);
                }
                else
                {
                    dataField.IsReadOnly = new LiteralValue(true);
                }

                var type = property.PropertyType;
                if (attr.DefaultValue != null)
                {
                    dataField.DefaultValue = TypeUtilities.GetResource<object>(attr.DefaultValue, null, deserializer);
                }
                else if (!type.IsValueType)
                {
                    // Null for reference types and nullables.
                    dataField.DefaultValue = LiteralValue.Null;
                } else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // Same for nullables.
                    dataField.DefaultValue = LiteralValue.Null;
                }
            }
        }
    }
}
