using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Resources;
using MaterialForms.Wpf.Validation;

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
            fieldFactories = new Dictionary<Type, Func<PropertyInfo, IEnumerable<FormElement>>>();
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
                        field.Name = GetStringResource(attr.Name);
                        field.ToolTip = GetStringResource(attr.Tooltip);
                        field.Icon = GetResource<PackIconKind>(attr.Icon, null);
                        field.IsReadOnly = GetResource<bool>(attr.IsReadOnly, false);
                        break;
                    case ValueAttribute attr:

                        break;
                    default:
                        continue;
                }
            }
        }

        private void InitializeFormField(FormField field, PropertyInfo propertyInfo)
        {

        }

        private IValidatorProvider CreateValidator(string propertyKey, ValueAttribute attribute)
        {
            Func<FrameworkElement, IProxy> argumentProvider;
            var argument = attribute.Argument;
            if (argument is string expression)
            {
                var boundExpression = BoundExpression.Parse(expression);
                if (boundExpression.IsPlainString)
                {
                    var literal = new PlainObject(boundExpression.StringFormat);
                    argumentProvider = container => literal;
                }
                else if (boundExpression.StringFormat != null)
                {
                    argumentProvider = container => boundExpression.GetStringValue(container);
                }
                else
                {
                    argumentProvider = container => boundExpression.GetValue(container);
                }
            }
            else
            {
                var literal = new PlainObject(argument);
                argumentProvider = container => literal;
            }

            Func<FrameworkElement, IProxy> valueProvider = container =>
            {
                var key = new BindingProxyKey(propertyKey);
                if (container.TryFindResource(key) is BindingProxy proxy)
                {
                    return proxy;
                }

                proxy = new BindingProxy();
                container.Resources[key] = proxy;
                return proxy;
            };

            Func<FrameworkElement, IBoolProxy> isEnforcedProvider;
            if (attribute.When != null)
            {
                var boundExpression = BoundExpression.Parse(attribute.When);
                if (!boundExpression.IsSingleResource)
                {
                    throw new ArgumentException(
                        "The provided value must be a bound resource or a literal bool value.", nameof(attribute));
                }

                isEnforcedProvider = container => boundExpression.GetBoolValue(container);
            }
            else
            {
                bool isEnforced;
                if (attribute.When is bool b)
                {
                    isEnforced = b;
                }
                else
                {
                    isEnforced = true;
                }

                var literal = new PlainBool(isEnforced);
                isEnforcedProvider = container => literal;
            }

            Func<FrameworkElement, IErrorStringProvider> errorProvider = null;

            IValueConverter GetConverter(FrameworkElement container)
            {
                IValueConverter converter = null;
                if (attribute.Converter != null)
                {
                    converter = Resource.GetValueConverter(container, attribute.Converter);
                }

                return converter;
            }

            switch (attribute.Condition)
            {
                case Must.BeEqualTo:
                    return new ValidatorProvider(container => new EqualsValidator(argumentProvider(container), errorProvider(container), GetConverter(container)));
                case Must.NotBeEqualTo:
                    break;
                case Must.BeGreaterThan:
                    break;
                case Must.BeGreaterThanOrEqualTo:
                    break;
                case Must.BeLessThan:
                    break;
                case Must.BeLessThanOrEqualTo:
                    break;
                case Must.BeEmpty:
                    break;
                case Must.NotBeEmpty:
                    break;
                case Must.BeTrue:
                    break;
                case Must.BeFalse:
                    break;
                case Must.BeNull:
                    break;
                case Must.NotBeNull:
                    break;
                case Must.ExistIn:
                    break;
                case Must.NotExistIn:
                    break;
                case Must.MatchPattern:
                    break;
                case Must.NotMatchPattern:
                    break;
                default:
                    throw new ArgumentException("Invalid validator condition.", nameof(attribute));
            }

            return null;
        }

        private class ValidatorProvider : IValidatorProvider
        {
            private readonly Func<FrameworkElement, ValidationRule> func;

            public ValidatorProvider(Func<FrameworkElement, ValidationRule> func)
            {
                this.func = func;
            }

            public ValidationRule GetValidator(FrameworkElement container)
            {
                return func(container);
            }
        }

        private IValueProvider GetResource<T>(object value, object defaultValue)
        {
            if (value == null)
            {
                return new LiteralValue(defaultValue);
            }

            if (value is string expression)
            {
                var boundExpression = BoundExpression.Parse(expression);
                if (!boundExpression.IsSingleResource)
                {
                    throw new ArgumentException($"The expression '{expression}' is not a valid resource because it does not define a single value source.",
                        nameof(value));
                }

                return new CoercedValueProvider<T>(boundExpression.Resources[0], defaultValue);
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
            if (expression == null)
            {
                return new LiteralValue(null);
            }

            var boundExpression = BoundExpression.Parse(expression);
            if (boundExpression.IsPlainString)
            {
                return new LiteralValue(boundExpression.StringFormat);
            }

            return boundExpression;
        }
    }
}
