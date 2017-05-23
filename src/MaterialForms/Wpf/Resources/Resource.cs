using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using MaterialForms.Wpf.Resources.ValueConverters;

namespace MaterialForms.Wpf.Resources
{
    public abstract class Resource : IEquatable<Resource>
    {
        public static IValueConverter GetConverter(string name)
        {
            return ValueConverters.TryGetValue(name, out var converter)
                ? converter
                : null;
        }

        /// <summary>
        /// Global cache for value converters accessible from expressions.
        /// </summary>
        public static readonly Dictionary<string, IValueConverter> ValueConverters =
            new Dictionary<string, IValueConverter>
            {
                ["IsNull"] = new IsNullConverter(),
                ["IsNotNull"] = new IsNotNullConverter(),
                ["IsEmpty"] = new IsEmptyConverter(),
                ["IsNotEmpty"] = new IsNotEmptyConverter(),
                ["ToUpper"] = new ToUpperConverter(),
                ["ToLower"] = new ToLowerConverter(),
                ["Length"] = new LengthValueConverter()
            };

        protected Resource(string valueConverter)
        {
            ValueConverter = valueConverter;
        }

        public string ValueConverter { get; }

        public abstract bool IsDynamic { get; }

        public abstract bool Equals(Resource other);

        public abstract BindingBase GetBinding(FrameworkElement element);

        public abstract Resource Rewrap(string valueConverter);

        protected IValueConverter GetValueConverter(FrameworkElement element)
        {
            if (string.IsNullOrEmpty(ValueConverter))
            {
                return null;
            }

            if (ValueConverters.TryGetValue(ValueConverter, out var c))
            {
                return c;
            }

            if (element != null && element.TryFindResource(ValueConverter) is IValueConverter converter)
            {
                return converter;
            }

            throw new InvalidOperationException($"Value converter '{ValueConverter}' not found.");
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
            if (String.IsNullOrEmpty(path))
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
