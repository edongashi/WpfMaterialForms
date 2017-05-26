using System;
using System.Collections.Generic;
using System.Reflection;
using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf
{
    public class FormBuilder
    {
        private readonly Dictionary<Type, FormDefinition> cachedDefinitions;
        private readonly Dictionary<Type, Func<PropertyInfo, IEnumerable<FormElement>>> fieldFactories;

        public static readonly FormBuilder Default = new FormBuilder();
        
        public FormBuilder()
        {
            cachedDefinitions = new Dictionary<Type, FormDefinition>();
            LoadDefaultFactories();
        }
        
        public FormDefinition GetDefinition(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            FormDefinition formDefinition;
            if (cachedDefinitions.TryGetValue(type, out formDefinition))
            {
                return formDefinition;
            }

            formDefinition = BuildDefinition(type);
            cachedDefinitions[type] = formDefinition;
            return formDefinition;
        }

        private FormDefinition BuildDefinition(Type type)
        {
            return null;
        }

        private List<FormElement> GetFormElements(Type type)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var elements = new List<FormElement>();
            foreach (var property in properties)
            {
                var fields = GetFieldElements(property);
                if (fields != null)
                {
                    elements.AddRange(fields);
                }
            }

            return elements;
        }

        private IEnumerable<FormElement> GetFieldElements(PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;
            if (fieldFactories.TryGetValue(type, out var factory))
            {
                return factory(propertyInfo);
            }
            
            // TODO: handle nested types.
            throw new NotImplementedException();
        }

        private void LoadDefaultFactories()
        {
            fieldFactories[typeof(string)] = GetStringField;
        }

        private StringField[] GetStringField(PropertyInfo propertyInfo)
        {
            return null;
        }

        private void InitializeDataField(DataFormField field, PropertyInfo propertyInfo)
        {
            foreach (var attribute in propertyInfo.GetCustomAttributes())
            {
                switch (attribute)
                {
                    case FieldAttribute attr:
                        if (attr.Name != null)
                        {
                            field.Name = GetStringResource(attr.Name);
                        }

                        field.Icon = GetResource<PackIconKind?>(attr.Icon, null);

                        break;
                    default:
                        continue;
                }
            }
        }

        private void InitializeFormField(FormField field, PropertyInfo propertyInfo)
        {
            
        }

        private IValueProvider GetResource<T>(object value, T defaultValue)
        {
            if (value == null)
            {
                return new LiteralValue(defaultValue);
            }

            if (value is string expression)
            {
                var boundExpression = BoundExpression.Parse(expression);
                if (boundExpression.Resources == null || boundExpression.Resources.Count != 1)
                {
                    throw new ArgumentException($"The expression '{expression}' is not a valid resource because it does not define a single value source.",
                        nameof(value));
                }

                return new CoercedValueProvider<T>(boundExpression, defaultValue);
            }

            if (value is T)
            {
                return new LiteralValue(value);
            }

            throw new ArgumentException(
                $"The provided value must be a bound resource or a literal value of type '{typeof(T).FullName}'.",
                nameof(value));
        }

        private IValueProvider GetStringResource(string expression)
        {
            var boundExpression = BoundExpression.Parse(expression);
            if (boundExpression.IsPlainString)
            {
                return new LiteralValue(boundExpression.StringFormat);
            }

            return boundExpression;
        }
    }
}
