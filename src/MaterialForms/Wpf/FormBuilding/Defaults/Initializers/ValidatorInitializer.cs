using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Resources;
using MaterialForms.Wpf.Validation;

namespace MaterialForms.Wpf.FormBuilding.Defaults.Initializers
{
    internal class ValidatorInitializer : IFieldInitializer
    {
        public void Initialize(FormElement element, PropertyInfo property, Func<string, object> deserializer)
        {
            if (!(element is DataFormField dataField))
            {
                return;
            }

            var attributes = property.GetCustomAttributes<ValueAttribute>().ToArray();
            if (attributes.Length == 0)
            {
                return;
            }

            var modelType = property.DeclaringType;
            foreach (var attr in attributes)
            {
                dataField.Validators.Add(CreateValidator(modelType, dataField.Key, attr, deserializer));
            }
        }

        private static IValidatorProvider CreateValidator(Type modelType, string propertyKey, ValueAttribute attribute, Func<string, object> deserializer)
        {
            Func<IResourceContext, IProxy> argumentProvider;
            var argument = attribute.Argument;
            if (argument is string expression)
            {
                var boundExpression = BoundExpression.Parse(expression);
                if (boundExpression.IsPlainString)
                {
                    var literal = new PlainObject(deserializer != null
                        ? deserializer(boundExpression.StringFormat)
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
    }
}
