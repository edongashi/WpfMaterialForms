using System;
using System.Collections.Generic;
using System.Linq;
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
        public static readonly FormBuilder Default = new FormBuilder();

        private readonly Dictionary<Type, FormDefinition> cachedDefinitions;
        private readonly Dictionary<Type, Func<PropertyInfo, IEnumerable<FormElement>>> fieldFactories;

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

            BindingProxy ValueProvider(FrameworkElement container)
            {
                var key = new BindingProxyKey(propertyKey);
                if (container.TryFindResource(key) is BindingProxy proxy)
                {
                    return proxy;
                }

                proxy = new BindingProxy();
                container.Resources[key] = proxy;
                return proxy;
            }

            Func<FrameworkElement, IBoolProxy> isEnforcedProvider;
            switch (attribute.When)
            {
                case null:
                    isEnforcedProvider = container => new PlainBool(true);
                    break;
                case string expr:
                    var boundExpression = BoundExpression.Parse(expr);
                    if (!boundExpression.IsSingleResource)
                    {
                        throw new ArgumentException(
                            "The provided value must be a bound resource or a literal bool value.", nameof(attribute));
                    }

                    isEnforcedProvider = container => boundExpression.GetBoolValue(container);
                    break;
                case bool b:
                    isEnforcedProvider = container => new PlainBool(b);
                    break;
                default:
                    throw new ArgumentException(
                        "The provided value must be a bound resource or a literal bool value.", nameof(attribute));

            }

            Func<FrameworkElement, IErrorStringProvider> errorProvider;
            var message = attribute.Message;
            if (message == null)
            {
                switch (attribute.Condition)
                {
                    case Must.BeGreaterThan:
                        message = "Value must be greater than {Argument}.";
                        break;
                    case Must.BeGreaterThanOrEqualTo:
                        message = "Value must be greater than or equal to {Argument}.";
                        break;
                    case Must.BeLessThan:
                        message = "Value must be less than {Argument}.";
                        break;
                    case Must.BeLessThanOrEqualTo:
                        message = "Value must be less than or equal to {Argument}.";
                        break;
                    case Must.BeEmpty:
                        message = "@Field must be empty.";
                        break;
                    case Must.NotBeEmpty:
                        message = "@Field cannot be empty.";
                        break;
                    default:
                        message = "@Invalid value.";
                        break;
                }
            }

            {
                var func = new Func<FrameworkElement, IProxy>(ValueProvider);
                var boundExpression = BoundExpression.Parse(message, new Dictionary<string, object>
                {
                    ["Value"] = func,
                    ["Argument"] = argumentProvider
                });

                if (boundExpression.IsPlainString)
                {
                    var errorMessage = boundExpression.StringFormat;
                    errorProvider = container => new PlainErrorStringProvider(errorMessage);
                }
                else
                {
                    if (boundExpression.Resources.Any(
                        res => res is DeferredProxyResource resource && resource.ProxyProvider == func))
                    {
                        errorProvider =
                            container => new ValueErrorStringProvider(boundExpression.GetStringValue(container),
                                ValueProvider(container));
                    }
                    else
                    {
                        errorProvider =
                            container => new ErrorStringProvider(boundExpression.GetStringValue(container));
                    }
                }
            }

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
                    return new ValidatorProvider(container => new EqualsValidator(argumentProvider(container),
                        errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                case Must.NotBeEqualTo:
                    return new ValidatorProvider(container => new NotEqualsValidator(argumentProvider(container),
                        errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                case Must.BeGreaterThan:
                    return new ValidatorProvider(container => new GreaterThanValidator(argumentProvider(container),
                        errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                case Must.BeGreaterThanOrEqualTo:
                    return new ValidatorProvider(container => new GreaterThanEqualValidator(argumentProvider(container),
                        errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                case Must.BeLessThan:
                    return new ValidatorProvider(container => new LessThanValidator(argumentProvider(container),
                        errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                case Must.BeLessThanOrEqualTo:
                    return new ValidatorProvider(container => new LessThanEqualValidator(argumentProvider(container),
                        errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                case Must.BeEmpty:
                    return new ValidatorProvider(container => new EmptyValidator(errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                case Must.NotBeEmpty:
                    return new ValidatorProvider(container => new NotEmptyValidator(errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                case Must.BeTrue:
                    return new ValidatorProvider(container => new TrueValidator(errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                case Must.BeFalse:
                    return new ValidatorProvider(container => new FalseValidator(errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                case Must.BeNull:
                    return new ValidatorProvider(container => new NullValidator(errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                case Must.NotBeNull:
                    return new ValidatorProvider(container => new NotNullValidator(errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                case Must.ExistIn:
                    return new ValidatorProvider(container => new ExistsInValidator(argumentProvider(container),
                        errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                case Must.NotExistIn:
                    return new ValidatorProvider(container => new NotExistsInValidator(argumentProvider(container),
                        errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                case Must.MatchPattern:
                    return new ValidatorProvider(container => new MatchPatternValidator(argumentProvider(container),
                        errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                case Must.NotMatchPattern:
                    return new ValidatorProvider(container => new NotMatchPatternValidator(argumentProvider(container),
                        errorProvider(container), isEnforcedProvider(container), GetConverter(container)));
                default:
                    throw new ArgumentException("Invalid validator condition.", nameof(attribute));
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
                    throw new ArgumentException(
                        $"The expression '{expression}' is not a valid resource because it does not define a single value source.",
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
    }
}
