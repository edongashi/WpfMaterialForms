using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
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

        #region Config

        private readonly Dictionary<Type, FormDefinition> cachedDefinitions;

        public FormBuilder()
        {
            cachedDefinitions = new Dictionary<Type, FormDefinition>();
            FieldFactories = new Dictionary<Type, Func<PropertyInfo, FormElement>>();
            TypeDeserializers = new Dictionary<Type, Func<string, object>>
            {
                [typeof(DateTime)] = str => DateTime.Parse(str, CultureInfo.InvariantCulture)
            };

            LoadDefaultFactories();
        }

        public Dictionary<Type, Func<PropertyInfo, FormElement>> FieldFactories { get; }

        /// <summary>
        /// Stores functions to parse string representations of types.
        /// </summary>
        public Dictionary<Type, Func<string, object>> TypeDeserializers { get; }

        public void InvalidateCache()
        {
            cachedDefinitions.Clear();
        }

        #endregion

        #region Reflection

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
            var formDefinition = new FormDefinition(type);
            var optIn = false;
            foreach (var attribute in type.GetCustomAttributes())
            {
                switch (attribute)
                {
                    case ResourceAttribute resource:
                        formDefinition.Resources.Add(resource.Name, resource.Value is string expr
                            ? (IValueProvider)BoundExpression.Parse(expr)
                            : new LiteralValue(resource.Value));
                        break;
                    case MessagesAttribute messages:
                        if (messages.Title != null)
                        {
                            formDefinition.TitleMessage = BoundExpression.Parse(messages.Title);
                        }

                        if (messages.Create != null)
                        {
                            formDefinition.CreateMessage = BoundExpression.Parse(messages.Create);
                        }

                        if (messages.Delete != null)
                        {
                            formDefinition.DeleteMessage = BoundExpression.Parse(messages.Delete);
                        }

                        if (messages.Details != null)
                        {
                            formDefinition.DetailsMessage = BoundExpression.Parse(messages.Details);
                        }

                        if (messages.Edit != null)
                        {
                            formDefinition.EditMessage = BoundExpression.Parse(messages.Edit);
                        }

                        break;
                    case FormAttribute form:
                        optIn = form.FieldGeneration == FieldGeneration.OptIn;
                        break;
                }
            }

            formDefinition.FormElements = GetFormElements(type, optIn);
            return formDefinition;
        }

        private List<FormElement> GetFormElements(Type type, bool optIn)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var elements = new List<FormElement>();
            foreach (var property in properties)
            {
                var element = GetFieldElement(property, optIn);
                if (element != null)
                {
                    elements.Add(element);
                }
            }

            return elements;
        }

        private FormElement GetFieldElement(PropertyInfo propertyInfo, bool optIn)
        {
            var type = propertyInfo.PropertyType;
            if (FieldFactories.TryGetValue(type, out var factory))
            {
                return factory(propertyInfo);
            }

            // TODO: handle nested types.
            throw new NotImplementedException();
        }

        private void LoadDefaultFactories()
        {
            FieldFactories[typeof(string)] = GetStringField;
        }

        private StringField GetStringField(PropertyInfo propertyInfo)
        {
            return null;
        }

        private void InitializeDataField(DataFormField field, PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;
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
                        field.Validators.Add(CreateValidator(field.Key, type, attr));
                        break;
                    default:
                        continue;
                }
            }
        }

        private void InitializeFormField(FormField field, PropertyInfo propertyInfo)
        {
        }

        #endregion

        #region Resources

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

        #endregion

        #region Validators

        private IValidatorProvider CreateValidator(string propertyKey, Type propertyType, ValueAttribute attribute)
        {
            Func<IResourceContext, IProxy> argumentProvider;
            var argument = attribute.Argument;
            if (argument is string expression)
            {
                var boundExpression = BoundExpression.Parse(expression);
                if (boundExpression.IsPlainString)
                {
                    var literal = new PlainObject(TypeDeserializers.TryGetValue(propertyType, out var func)
                        ? func(boundExpression.StringFormat)
                        : boundExpression.StringFormat);

                    argumentProvider = context => literal;
                }
                else if (boundExpression.StringFormat != null)
                {
                    argumentProvider = context => boundExpression.GetStringValue(context);
                }
                else
                {
                    argumentProvider = context => boundExpression.GetValue(context);
                }
            }
            else
            {
                var literal = new PlainObject(argument);
                argumentProvider = context => literal;
            }

            BindingProxy ValueProvider(IResourceContext context)
            {
                var key = new BindingProxyKey(propertyKey);
                if (context.TryFindResource(key) is BindingProxy proxy)
                {
                    return proxy;
                }

                proxy = new BindingProxy();
                context.AddResource(key, proxy);
                return proxy;
            }

            Func<IResourceContext, IBoolProxy> isEnforcedProvider;
            switch (attribute.When)
            {
                case null:
                    isEnforcedProvider = context => new PlainBool(true);
                    break;
                case string expr:
                    var boundExpression = BoundExpression.Parse(expr);
                    if (!boundExpression.IsSingleResource)
                    {
                        throw new ArgumentException(
                            "The provided value must be a bound resource or a literal bool value.", nameof(attribute));
                    }

                    isEnforcedProvider = context => boundExpression.GetBoolValue(context);
                    break;
                case bool b:
                    isEnforcedProvider = context => new PlainBool(b);
                    break;
                default:
                    throw new ArgumentException(
                        "The provided value must be a bound resource or a literal bool value.", nameof(attribute));
            }

            Func<IResourceContext, IErrorStringProvider> errorProvider;
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
                var func = new Func<IResourceContext, IProxy>(ValueProvider);
                var boundExpression = BoundExpression.Parse(message, new Dictionary<string, object>
                {
                    ["Value"] = func,
                    ["Argument"] = argumentProvider
                });

                if (boundExpression.IsPlainString)
                {
                    var errorMessage = boundExpression.StringFormat;
                    errorProvider = context => new PlainErrorStringProvider(errorMessage);
                }
                else
                {
                    if (boundExpression.Resources.Any(
                        res => res is DeferredProxyResource resource && resource.ProxyProvider == func))
                    {
                        errorProvider =
                            context => new ValueErrorStringProvider(boundExpression.GetStringValue(context),
                                ValueProvider(context));
                    }
                    else
                    {
                        errorProvider =
                            context => new ErrorStringProvider(boundExpression.GetStringValue(context));
                    }
                }
            }

            var converterName = attribute.Converter;
            IValueConverter GetConverter(IResourceContext context)
            {
                IValueConverter converter = null;
                if (converterName != null)
                {
                    converter = Resource.GetValueConverter(context, converterName);
                }

                return converter;
            }

            switch (attribute.Condition)
            {
                case Must.BeEqualTo:
                    return new ValidatorProvider(context => new EqualsValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context)));
                case Must.NotBeEqualTo:
                    return new ValidatorProvider(context => new NotEqualsValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context)));
                case Must.BeGreaterThan:
                    return new ValidatorProvider(context => new GreaterThanValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context)));
                case Must.BeGreaterThanOrEqualTo:
                    return new ValidatorProvider(context => new GreaterThanEqualValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context)));
                case Must.BeLessThan:
                    return new ValidatorProvider(context => new LessThanValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context)));
                case Must.BeLessThanOrEqualTo:
                    return new ValidatorProvider(context => new LessThanEqualValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context)));
                case Must.BeEmpty:
                    return new ValidatorProvider(context => new EmptyValidator(errorProvider(context),
                        isEnforcedProvider(context), GetConverter(context)));
                case Must.NotBeEmpty:
                    return new ValidatorProvider(context => new NotEmptyValidator(errorProvider(context),
                        isEnforcedProvider(context), GetConverter(context)));
                case Must.BeTrue:
                    return new ValidatorProvider(context => new TrueValidator(errorProvider(context),
                        isEnforcedProvider(context), GetConverter(context)));
                case Must.BeFalse:
                    return new ValidatorProvider(context => new FalseValidator(errorProvider(context),
                        isEnforcedProvider(context), GetConverter(context)));
                case Must.BeNull:
                    return new ValidatorProvider(context => new NullValidator(errorProvider(context),
                        isEnforcedProvider(context), GetConverter(context)));
                case Must.NotBeNull:
                    return new ValidatorProvider(context => new NotNullValidator(errorProvider(context),
                        isEnforcedProvider(context), GetConverter(context)));
                case Must.ExistIn:
                    return new ValidatorProvider(context => new ExistsInValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context)));
                case Must.NotExistIn:
                    return new ValidatorProvider(context => new NotExistsInValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context)));
                case Must.MatchPattern:
                    return new ValidatorProvider(context => new MatchPatternValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context)));
                case Must.NotMatchPattern:
                    return new ValidatorProvider(context => new NotMatchPatternValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context)));
                default:
                    throw new ArgumentException("Invalid validator condition.", nameof(attribute));
            }
        }

        private class ValidatorProvider : IValidatorProvider
        {
            private readonly Func<IResourceContext, ValidationRule> func;

            public ValidatorProvider(Func<IResourceContext, ValidationRule> func)
            {
                this.func = func;
            }

            public ValidationRule GetValidator(IResourceContext context)
            {
                return func(context);
            }
        }

        #endregion
    }
}
