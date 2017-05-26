using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using MaterialForms.Wpf.Resources.ValueConverters;

namespace MaterialForms.Wpf.Resources
{
    public abstract class Resource : IEquatable<Resource>, IValueProvider
    {
        /// <summary>
        /// Global cache for value converters accessible from expressions.
        /// </summary>
        public static readonly Dictionary<string, IValueConverter> ValueConverters =
            new Dictionary<string, IValueConverter>
            {
                ["IsNull"] = new IsNullConverter(),
                ["IsNotNull"] = new IsNotNullConverter(),
                ["AsBool"] = new AsBoolConverter(),
                ["IsEmpty"] = new IsEmptyConverter(),
                ["IsNotEmpty"] = new IsNotEmptyConverter(),
                ["ToUpper"] = new ToUpperConverter(),
                ["ToLower"] = new ToLowerConverter(),
                ["Length"] = new LengthValueConverter(),
                ["ToString"] = new ToStringConverter()
            };

        protected Resource(string valueConverter)
        {
            ValueConverter = valueConverter;
        }

        public string ValueConverter { get; }

        public abstract bool IsDynamic { get; }

        public abstract bool Equals(Resource other);

        public abstract BindingBase ProvideBinding(FrameworkElement container);

        public virtual object ProvideValue(FrameworkElement container) => ProvideBinding(container);

        public abstract Resource Rewrap(string valueConverter);

        protected IValueConverter GetValueConverter(FrameworkElement container)
        {
            return GetValueConverter(container, ValueConverter);
        }

        protected internal static IValueConverter GetValueConverter(FrameworkElement container, string valueConverter)
        {
            if (string.IsNullOrEmpty(valueConverter))
            {
                return null;
            }

            if (ValueConverters.TryGetValue(valueConverter, out var c))
            {
                return c;
            }

            if (container != null && container.TryFindResource(valueConverter) is IValueConverter converter)
            {
                return converter;
            }

            throw new InvalidOperationException($"Value converter '{valueConverter}' not found.");
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Resource)obj);
        }

        public abstract override int GetHashCode();

        protected static string FormatPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }

            if (path[0] == '[')
            {
                return path;
            }

            return "." + path;
        }
    }
}
