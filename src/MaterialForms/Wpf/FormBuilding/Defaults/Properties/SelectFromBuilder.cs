using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Humanizer;
using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Fields.Defaults;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.FormBuilding.Defaults.Properties
{
    internal class SelectFromBuilder : IFieldBuilder
    {
        public FormElement TryBuild(IFormProperty property, Func<string, object> deserializer)
        {
            var selectFrom = property.GetCustomAttribute<SelectFromAttribute>();
            if (selectFrom == null)
            {
                return null;
            }

            var type = property.PropertyType;
            var field = new SelectionField(property.Name, property.PropertyType);
            if (selectFrom.DisplayPath != null)
            {
                field.DisplayPath = BoundExpression.ParseSimplified(selectFrom.DisplayPath);
            }

            if (selectFrom.ValuePath != null)
            {
                field.ValuePath = BoundExpression.ParseSimplified(selectFrom.ValuePath);
            }

            if (selectFrom.ItemStringFormat != null)
            {
                field.ItemStringFormat = BoundExpression.ParseSimplified(selectFrom.ItemStringFormat);
            }

            field.SelectionType = Utilities.GetResource<SelectionType>(selectFrom.SelectionType, SelectionType.ComboBox, Deserializers.Enum<SelectionType>());

            switch (selectFrom.ItemsSource)
            {
                case string expr:
                    var value = BoundExpression.Parse(expr);
                    if (!value.IsSingleResource)
                    {
                        throw new InvalidOperationException("ItemsSource must be a single resource reference.");
                    }

                    field.ItemsSource = value.Resources[0];
                    break;
                case IEnumerable<object> enumerable:
                    field.ItemsSource = new LiteralValue(enumerable.ToList());
                    break;
                case Type enumType:
                    if (!enumType.IsEnum)
                    {
                        throw new InvalidOperationException("A type argument for ItemsSource must be an enum.");
                    }

                    var values = Enum.GetValues(enumType);
                    var collection = new List<KeyValuePair<ValueType, IValueProvider>>();
                    foreach (Enum enumValue in values)
                    {
                        var enumName = enumValue.ToString();
                        var memInfo = enumType.GetMember(enumName);
                        var attributes = memInfo[0].GetCustomAttributes(typeof(EnumDisplayAttribute), false);
                        IValueProvider name;
                        if (attributes.Length > 0)
                        {
                            var attr = (EnumDisplayAttribute)attributes[0];
                            name = BoundExpression.ParseSimplified(attr.Name);
                        }
                        else
                        {
                            name = new LiteralValue(enumName.Humanize());
                        }

                        collection.Add(new KeyValuePair<ValueType, IValueProvider>(enumValue, name));
                    }

                    field.ItemsSource = new EnumerableStringValueProvider(collection);
                    field.DisplayPath = new LiteralValue(nameof(StringProxy.Value));
                    field.ValuePath = new LiteralValue(type == typeof(string)
                        ? nameof(StringProxy.Value)
                        : nameof(StringProxy.Key));
                    break;
            }

            return field;
        }
    }
}
