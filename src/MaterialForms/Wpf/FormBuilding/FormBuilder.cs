using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using Humanizer;
using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Fields.Implementations;
using MaterialForms.Wpf.Resources;
using MaterialForms.Wpf.Validation;

namespace MaterialForms.Wpf.FormBuilding
{
    public class FormBuilder
    {
        public static readonly FormBuilder Default = new FormBuilder();

        #region Config

        private readonly Dictionary<Type, FormDefinition> cachedDefinitions;

        private readonly Dictionary<Type, Func<FormBuilder, PropertyInfo, FormElement>> fieldFactories;

        /// <summary>
        /// Stores functions to parse string representations of types.
        /// </summary>
        private readonly Dictionary<Type, Func<string, object>> typeDeserializers;

        public FormBuilder()
        {
            cachedDefinitions = new Dictionary<Type, FormDefinition>();
            fieldFactories = new Dictionary<Type, Func<FormBuilder, PropertyInfo, FormElement>>();
            typeDeserializers = new Dictionary<Type, Func<string, object>>
            {
                [typeof(DateTime)] = str => DateTime.Parse(str, CultureInfo.InvariantCulture)
            };

            LoadDefaultFactories();
        }

        public ForPropertySyntax ForProperty<TProperty>()
        {
            return new ForPropertySyntax(this, typeof(TProperty));
        }

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

            if (cachedDefinitions.TryGetValue(type, out var formDefinition))
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
            var mode = DefaultFields.AllExcludingReadonly;
            foreach (var attribute in type.GetCustomAttributes())
            {
                switch (attribute)
                {
                    case ResourceAttribute resource:
                        formDefinition.Resources.Add(resource.Name, resource.Value is string expr
                            ? (IValueProvider)BoundExpression.Parse(expr)
                            : new LiteralValue(resource.Value));
                        break;
                    case FormAttribute form:
                        mode = form.Mode;
                        break;
                }
            }

            if (formDefinition.TitleMessage == null)
            {
                formDefinition.TitleMessage = new LiteralValue(type.Name.Humanize());
            }

            var properties = TypeUtilities.GetProperties(type, mode);
            return formDefinition;
        }

        public string Test { get; set; }

        private List<FormElement> GetFormElements(Type type, PropertyInfo property)
        {
            throw new NotImplementedException();
        }

        private List<FormElement> GetFormElements(Type modelType, bool optIn)
        {
            var properties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var elements = new List<FormElement>();
            foreach (var property in properties)
            {
                var element = GetFieldElement(modelType, property, optIn);
                if (element != null)
                {
                    elements.Add(element);
                }
            }

            return elements;
        }

        private FormElement GetFieldElement(Type modelType, PropertyInfo propertyInfo, bool optIn)
        {
            var type = propertyInfo.PropertyType;
            var add = !optIn;
            SelectFromAttribute selectFrom = null;
            foreach (var attribute in propertyInfo.GetCustomAttributes())
            {
                switch (attribute)
                {
                    case FieldAttribute _:
                        add = true;
                        break;
                    case SelectFromAttribute attr:
                        selectFrom = attr;
                        break;
                    case FieldIgnoreAtribute _:
                        return null;
                }
            }

            if (!add)
            {
                return null;
            }

            if (selectFrom != null)
            {
                return GetSelectionField(modelType, propertyInfo, selectFrom);
            }
            if (fieldFactories.TryGetValue(type, out var factory))
            {
                return factory(this, propertyInfo);
            }
            // TODO: handle nested types.
            return null;
        }

        private FormElement GetSelectionField(Type modelType, PropertyInfo propertyInfo, SelectFromAttribute selectFrom)
        {
            var type = propertyInfo.PropertyType;
            var field = new SelectionField(propertyInfo.Name);
            InitializeField(modelType, field, propertyInfo, null);
            if (selectFrom.DisplayPath != null)
            {
                field.DisplayPath = BoundExpression.Parse(selectFrom.DisplayPath).Simplified();
            }

            if (selectFrom.ValuePath != null)
            {
                field.ValuePath = BoundExpression.Parse(selectFrom.ValuePath).Simplified();
            }

            if (selectFrom.ItemStringFormat != null)
            {
                field.ItemStringFormat = BoundExpression.Parse(selectFrom.ItemStringFormat).Simplified();
            }

            field.SelectionType = GetResource<SelectionType>(selectFrom.SelectionType, SelectionType.ComboBox);

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
                            name = BoundExpression.Parse(attr.Name).Simplified();
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

        private void LoadDefaultFactories()
        {
        }

        private void InitializeField(Type modelType, FormElement element, PropertyInfo propertyInfo,
            Action<Attribute> attributeInitializer)
        {
            var type = propertyInfo.PropertyType;
            foreach (var attribute in propertyInfo.GetCustomAttributes())
            {
                switch (attribute)
                {
                    case FieldAttribute attr:
                        if (element is FormField field)
                        {
                            field.Name = GetStringResource(attr.Name);
                            field.ToolTip = GetStringResource(attr.Tooltip);
                            field.Icon = GetResource<PackIconKind>(attr.Icon, null);
                            field.IsReadOnly = GetResource<bool>(attr.IsReadOnly, false);
                            field.DefaultValue = GetResource<object>(attr.DefaultValue, null);
                        }

                        element.GroupKey = attr.Group;
                        break;
                    case ValueAttribute attr:
                        if (element is DataFormField dataField)
                        {
                            dataField.Validators.Add(CreateValidator(modelType, dataField.Key, type, attr));
                        }

                        break;
                    default:
                        attributeInitializer?.Invoke(attribute);
                        break;
                }
            }
        }

        #endregion

        #region Resources

        private static IValueProvider GetResource<T>(object value, object defaultValue)
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

        private static IValueProvider GetStringResource(string expression)
        {
            return expression == null ? new LiteralValue(null) : BoundExpression.Parse(expression).Simplified();
        }

        #endregion

        #region Validators

        private IValidatorProvider CreateValidator(Type modelType, string propertyKey, Type propertyType, ValueAttribute attribute)
        {
            Func<IResourceContext, IProxy> argumentProvider;
            var argument = attribute.Argument;
            if (argument is string expression)
            {
                var boundExpression = BoundExpression.Parse(expression);
                if (boundExpression.IsPlainString)
                {
                    var literal = new PlainObject(typeDeserializers.TryGetValue(propertyType, out var func)
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

            var validationStep = attribute.ValidationStep;
            var validateOnTargetUpdated = attribute.ValidatesOnTargetUpdated;
            switch (attribute.Condition)
            {
                case Must.BeEqualTo:
                    return new ValidatorProvider(context => new EqualsValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.NotBeEqualTo:
                    return new ValidatorProvider(context => new NotEqualsValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.BeGreaterThan:
                    return new ValidatorProvider(context => new GreaterThanValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.BeGreaterThanOrEqualTo:
                    return new ValidatorProvider(context => new GreaterThanEqualValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.BeLessThan:
                    return new ValidatorProvider(context => new LessThanValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.BeLessThanOrEqualTo:
                    return new ValidatorProvider(context => new LessThanEqualValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.BeEmpty:
                    return new ValidatorProvider(context => new EmptyValidator(errorProvider(context),
                        isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.NotBeEmpty:
                    return new ValidatorProvider(context => new NotEmptyValidator(errorProvider(context),
                        isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.BeTrue:
                    return new ValidatorProvider(context => new TrueValidator(errorProvider(context),
                        isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.BeFalse:
                    return new ValidatorProvider(context => new FalseValidator(errorProvider(context),
                        isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.BeNull:
                    return new ValidatorProvider(context => new NullValidator(errorProvider(context),
                        isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.NotBeNull:
                    return new ValidatorProvider(context => new NotNullValidator(errorProvider(context),
                        isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.ExistIn:
                    return new ValidatorProvider(context => new ExistsInValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.NotExistIn:
                    return new ValidatorProvider(context => new NotExistsInValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.MatchPattern:
                    return new ValidatorProvider(context => new MatchPatternValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.NotMatchPattern:
                    return new ValidatorProvider(context => new NotMatchPatternValidator(argumentProvider(context),
                        errorProvider(context), isEnforcedProvider(context), GetConverter(context), validationStep,
                        validateOnTargetUpdated));
                case Must.SatisfyContextMethod:
                    var methodName = GetMethodName(attribute.Argument, propertyKey);
                    var propertyName = propertyKey;
                    return new ValidatorProvider(
                        context => new MethodInvocationValidator(GetContextMethodValidator(propertyName, methodName, context),
                            errorProvider(context), isEnforcedProvider(context), GetConverter(context), validationStep,
                            validateOnTargetUpdated));
                case Must.SatisfyMethod:
                    var type = modelType;
                    methodName = GetMethodName(attribute.Argument, propertyKey);
                    propertyName = propertyKey;
                    return new ValidatorProvider(
                        context => new MethodInvocationValidator(GetModelMethodValidator(type, propertyName, methodName, context),
                            errorProvider(context), isEnforcedProvider(context), GetConverter(context), validationStep,
                            validateOnTargetUpdated));
                default:
                    throw new ArgumentException($"Invalid validator condition for property {propertyKey}.",
                        nameof(attribute));
            }
        }

        private static string GetMethodName(object argument, string propertyKey)
        {
            if (argument is string methodName && !string.IsNullOrWhiteSpace(methodName))
            {
                return methodName;
            }

            throw new InvalidOperationException(
                $"Validator for property {propertyKey} does not specify a valid method name. Value must be a nonempty string.");
        }

        // Called on binding -> not performance critical.
        private static Func<object, CultureInfo, ValidationStep, bool> GetModelMethodValidator(Type modelType, string propertyName, string methodName, IResourceContext context)
        {
            var method = GetMethod(modelType, methodName);
            if (method == null)
            {
                throw new InvalidOperationException(
                    $"Type hierarchy of {modelType.FullName} does not include a static method named {methodName}.");
            }

            // Called on validation -> performance critical.
            bool Validate(object value, CultureInfo cultureInfo, ValidationStep validationStep)
            {
                return method(new ValidationContext(
                    context.GetModelInstance(),
                    context.GetContextInstance(),
                    propertyName,
                    value,
                    cultureInfo,
                    validationStep));
            }

            return Validate;
        }

        // Called on binding = not performance critical.
        private static Func<object, CultureInfo, ValidationStep, bool> GetContextMethodValidator(string propertyName, string methodName, IResourceContext context)
        {
            Type currentType = null;
            Func<ValidationContext, bool> method = null;

            // Called on validation -> performance critical.
            bool Validate(object value, CultureInfo cultureInfo, ValidationStep validationStep)
            {
                // Context type may change in runtime. Change delegate only when necessary.
                var contextInstance = context.GetContextInstance();
                var contextType = contextInstance?.GetType();
                if (contextType != currentType)
                {
                    method = GetMethod(contextType, methodName);
                    currentType = contextType;
                }

                if (method == null)
                {
                    return true;
                }

                return method(new ValidationContext(
                    context.GetModelInstance(),
                    contextInstance,
                    propertyName,
                    value,
                    cultureInfo,
                    validationStep));
            }

            return Validate;
        }

        private static Func<ValidationContext, bool> GetMethod(Type type, string methodName)
        {
            var delegateType = typeof(Func<ValidationContext, bool>);
            bool IsMatch(MethodInfo methodInfo)
            {
                if (methodInfo.Name != methodName)
                {
                    return false;
                }

                if (methodInfo.ReturnType != typeof(bool))
                {
                    return false;
                }

                var parameters = methodInfo.GetParameters();
                if (parameters.Length != 1)
                {
                    return false;
                }

                return parameters[0].ParameterType == typeof(ValidationContext);
            }

            var method = type
                .GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)
                .FirstOrDefault(IsMatch);

            if (method == null)
            {
                return null;
            }

            try
            {
                return (Func<ValidationContext, bool>)Delegate.CreateDelegate(delegateType, method);
            }
            catch
            {
                return null;
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
