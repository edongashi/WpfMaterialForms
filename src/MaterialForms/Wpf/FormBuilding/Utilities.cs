using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Resources;
using MaterialForms.Wpf.Validation;

namespace MaterialForms.Wpf.FormBuilding
{
    internal static class Utilities
    {
        public static List<PropertyInfo> GetProperties(Type type, DefaultFields mode)
        {
            if (type == null)
            {
                throw new ArgumentException(nameof(type));
            }

            // First requirement is that properties and getters must be public.
            var properties = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.GetGetMethod(true).IsPublic)
                .OrderBy(p => p.MetadataToken);

            switch (mode)
            {
                case DefaultFields.AllIncludingReadonly:
                    return properties
                        .Where(p => p.GetCustomAttribute<FieldIgnoreAtribute>() == null)
                        .ToList();
                case DefaultFields.AllExcludingReadonly:
                    return properties.Where(p =>
                    {
                        if (p.GetCustomAttribute<FieldIgnoreAtribute>() != null)
                        {
                            return false;
                        }

                        if (p.GetCustomAttribute<FieldAttribute>() != null)
                        {
                            return true;
                        }

                        return p.CanWrite && p.GetSetMethod(true).IsPublic;
                    }).ToList();
                case DefaultFields.None:
                    return properties.Where(p =>
                    {
                        if (p.GetCustomAttribute<FieldIgnoreAtribute>() != null)
                        {
                            return false;
                        }

                        return p.GetCustomAttribute<FieldAttribute>() != null;
                    }).ToList();
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, "Invalid DefaultFields value.");
            }
        }

        public static IValueProvider GetResource<T>(object value, object defaultValue, Func<string, object> deserializer)
        {
            if (value == null)
            {
                return new LiteralValue(defaultValue);
            }

            if (value is string expression)
            {
                var boundExpression = BoundExpression.Parse(expression);
                switch (boundExpression.Resources.Count)
                {
                    case 0 when deserializer != null:
                        return new LiteralValue(deserializer(expression));
                    case 1 when boundExpression.StringFormat == null:
                        return new CoercedValueProvider<T>(boundExpression.Resources[0], defaultValue);
                    default:
                        throw new ArgumentException(
                            $"The expression '{expression}' is not a valid resource because it does not define a single value source.",
                            nameof(value));
                }
            }

            if (value is T)
            {
                return new LiteralValue(value);
            }

            throw new ArgumentException(
                $"The provided value must be a bound resource or a literal value of type '{typeof(T).FullName}'.",
                nameof(value));
        }

        public static IValueProvider GetIconResource(object value)
        {
            if (value is -1 || value is string s && string.Equals(s, "empty", StringComparison.OrdinalIgnoreCase))
            {
                return new LiteralValue((PackIconKind)(-1));
            }

            return GetResource<PackIconKind>(value, (PackIconKind)(-2), Deserializers.Enum<PackIconKind>());
        }

        public static IValueProvider GetStringResource(string expression)
        {
            return expression == null ? new LiteralValue(null) : BoundExpression.ParseSimplified(expression);
        }

        public static BindingProxy GetValueProxy(IResourceContext context, string propertyKey)
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

        public static Func<IResourceContext, IProxy> GetValueProvider(string propertyKey)
        {
            BindingProxy ValueProvider(IResourceContext context)
            {
                return GetValueProxy(context, propertyKey);
            }

            return ValueProvider;
        }

        public static Func<IResourceContext, IErrorStringProvider> GetErrorProvider(string message, string propertyKey)
        {
            var func = GetValueProvider(propertyKey);
            var boundExpression = BoundExpression.Parse(message, new Dictionary<string, object>
            {
                ["Value"] = func
            });

            if (boundExpression.IsPlainString)
            {
                var errorMessage = boundExpression.StringFormat;
                return context => new PlainErrorStringProvider(errorMessage);
            }

            if (boundExpression.Resources.Any(
                res => res is DeferredProxyResource resource && resource.ProxyProvider == func))
            {
                var key = propertyKey;
                return context => new ValueErrorStringProvider(boundExpression.GetStringValue(context), GetValueProxy(context, key));
            }

            return context => new ErrorStringProvider(boundExpression.GetStringValue(context));
        }
    }
}
