using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using MaterialForms.Wpf.Resources.ValueConverters;

namespace MaterialForms.Wpf.Resources
{
    public class BoundExpression
    {
        public BoundExpression(string value)
        {
            StringFormat = value ?? throw new ArgumentNullException(nameof(value));
        }

        public BoundExpression(Resource resource) : this(resource, null)
        {
        }

        public BoundExpression(Resource resource, string stringFormat)
        {
            Resources = new List<Resource>(1) { resource ?? throw new ArgumentNullException(nameof(resource)) };
            StringFormat = stringFormat;
        }

        public BoundExpression(IEnumerable<Resource> resources, string stringFormat)
        {
            Resources = resources?.ToList() ?? throw new ArgumentNullException(nameof(resources));
            if (Resources.Count != 1)
            {
                StringFormat = stringFormat ?? throw new ArgumentNullException(nameof(stringFormat));
            }
            else
            {
                StringFormat = stringFormat;
            }
        }

        public string StringFormat { get; }

        public IReadOnlyList<Resource> Resources { get; }

        public bool IsDynamic => Resources != null && Resources.Any(res => res.IsDynamic);

        public void SetValue(FrameworkElement container, DependencyObject element, DependencyProperty property)
        {
            if (Resources == null || Resources.Count == 0)
            {
                element.SetValue(property, StringFormat);
                return;
            }

            if (Resources.Count == 1)
            {
                var resource = Resources[0];
                var binding = resource.GetBinding(container);
                if (StringFormat != null)
                {
                    binding.StringFormat = StringFormat;
                }

                BindingOperations.SetBinding(element, property, binding);
                return;
            }

            var multiBinding = new MultiBinding
            {
                StringFormat = StringFormat
            };

            foreach (var binding in Resources.Select(resource => resource.GetBinding(container)))
            {
                multiBinding.Bindings.Add(binding);
            }

            BindingOperations.SetBinding(element, property, multiBinding);
        }

        public BindingProxy GetValue(FrameworkElement container)
        {
            var proxy = new BindingProxy();
            SetValue(container, proxy, BindingProxy.ValueProperty);
            return proxy;
        }

        public StringProxy GetStringValue(FrameworkElement container)
        {
            var proxy = new StringProxy();
            SetValue(container, proxy, StringProxy.ValueProperty);
            return proxy;
        }

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
                ["ToLower"] = new ToLowerConverter()
            };

        public static BoundExpression Parse(string expression)
        {
            return Parse(expression, contextResource: null);
        }

        public static BoundExpression Parse(string expression, IDictionary<string, object> contextResources)
        {
            Resource Factory(string name, IValueConverter converter)
            {
                if (!contextResources.TryGetValue(name, out var value))
                {
                    return null;
                }

                switch (value)
                {
                    case Resource resource:
                        return resource.Rewrap(converter);
                    case BindingProxy proxy:
                        return new BindingProxyResource(proxy, false, converter);
                    case StringProxy proxy:
                        return new StringProxyResource(proxy, false, converter);
                    default:
                        return new LiteralValue(value, converter);
                }
            }

            return Parse(expression, Factory);
        }

        public static BoundExpression Parse(string expression, Func<string, IValueConverter, Resource> contextResource)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var resources = new List<Resource>();
            var stringFormat = new StringBuilder();
            var resourceType = new StringBuilder();
            var resourceName = new StringBuilder();
            var resourceConverter = new StringBuilder();
            var resourceFormat = new StringBuilder();
            var length = expression.Length;
            var i = 0;
            char c;
            outside:
            if (i == length)
            {
                var format = stringFormat.ToString();
                return new BoundExpression(resources, format == "{0}" ? null : format);
            }

            c = expression[i];
            if (c == '{')
            {
                stringFormat.Append('{');
                if (++i == expression.Length)
                {
                    throw new FormatException("Invalid unescaped '{' at end of input.");
                }

                if (expression[i] == '{')
                {
                    i++;
                    stringFormat.Append('{');
                    goto outside;
                }

                goto readResource;
            }

            if (c == '}')
            {
                stringFormat.Append('}');
                if (++i == expression.Length)
                {
                    throw new FormatException("Invalid unescaped '}' at end of input.");
                }

                if (expression[i] == '}')
                {
                    i++;
                    stringFormat.Append('}');
                    goto outside;
                }

                throw new FormatException("Invalid unescaped '}'.");
            }

            stringFormat.Append(c);
            i++;
            goto outside;

            readResource:

            // Resource type.
            while (char.IsLetter(c = expression[i]))
            {
                resourceType.Append(c);
                if (++i == length)
                {
                    throw new FormatException("Unexpected end of input.");
                }
            }

            // Skip whitespace.
            while (char.IsWhiteSpace(expression[i]))
            {
                if (++i == length)
                {
                    throw new FormatException("Unexpected end of input.");
                }
            }

            // Resource name.
            while ((c = expression[i]) != ',' && c != ':' && c != '|')
            {
                if (char.IsWhiteSpace(c))
                {
                    break;
                }

                if (c == '{')
                {
                    if (++i == length)
                    {
                        throw new FormatException("Unexpected end of input.");
                    }

                    if (expression[i] != '{')
                    {
                        throw new FormatException("Invalid unescaped '{'.");
                    }
                }
                else if (c == '}')
                {
                    if (++i == length)
                    {
                        goto addResource;
                    }

                    if (expression[i] != '}')
                    {
                        goto addResource;
                    }
                }

                resourceName.Append(c);
                if (++i == length)
                {
                    throw new FormatException("Unexpected end of input.");
                }
            }

            // Skip whitespace between name and converter/format/end.
            while (char.IsWhiteSpace(expression[i]))
            {
                if (++i == length)
                {
                    throw new FormatException("Unexpected end of input.");
                }
            }

            c = expression[i];
            if (c == '}')
            {
                // Resource can close at this point assuming no converter and no string format.
                if (++i == length)
                {
                    goto addResource;
                }

                if (expression[i] != '}')
                {
                    goto addResource;
                }

                throw new FormatException("Invalid '}}' sequence.");
            }

            // Value converter, read while character is letter.
            if (c == '|')
            {
                if (++i == length)
                {
                    throw new FormatException("Unexpected end of input.");
                }

                while (char.IsLetter(c = expression[i]))
                {
                    resourceConverter.Append(c);
                    if (++i == length)
                    {
                        throw new FormatException("Unexpected end of input.");
                    }
                }

                // Skip whitespace between converter to format/end.
                while (char.IsWhiteSpace(expression[i]))
                {
                    if (++i == length)
                    {
                        throw new FormatException("Unexpected end of input.");
                    }
                }

                if (c == '}')
                {
                    if (++i == length)
                    {
                        goto addResource;
                    }

                    if (expression[i] != '}')
                    {
                        goto addResource;
                    }

                    throw new FormatException("Converter name cannot contain braces.");
                }
            }

            // String format, read until single '}'.
            if (c == ',' || c == ':')
            {
                resourceFormat.Append(c);
                while (true)
                {
                    if (++i == length)
                    {
                        throw new FormatException("Unexpected end of input.");
                    }

                    c = expression[i];
                    if (c == '}')
                    {
                        if (++i == length)
                        {
                            goto addResource;
                        }

                        if (expression[i] != '}')
                        {
                            goto addResource;
                        }

                        resourceFormat.Append('}');
                    }

                    resourceFormat.Append(c);
                }
            }

            addResource:
            var key = resourceName.ToString();
            Resource resource;
            var converter = GetConverter(resourceConverter.ToString());
            var resourceTypeString = resourceType.ToString();
            switch (resourceTypeString)
            {
                case "Binding":
                    resource = new PropertyBinding(key, false, converter);
                    break;
                case "Property":
                    resource = new PropertyBinding(key, true, converter);
                    break;
                case "StaticResource":
                    resource = new StaticResource(key, converter);
                    break;
                case "DynamicResource":
                    resource = new DynamicResource(key, converter);
                    break;
                case "ContextBinding":
                    resource = new ContextPropertyBinding(key, false, converter);
                    break;
                case "ContextProperty":
                    resource = new ContextPropertyBinding(key, true, converter);
                    break;
                default:
                    resource = contextResource?.Invoke(resourceTypeString, converter);
                    if (resource != null)
                    {
                        break;
                    }

                    throw new FormatException("Invalid resource type.");
            }

            var index = resources.IndexOf(resource);
            if (index == -1)
            {
                index = resources.Count;
                resources.Add(resource);
            }

            stringFormat.Append(index);
            if (resourceFormat.Length != 0)
            {
                stringFormat.Append(resourceFormat);
            }

            stringFormat.Append('}');

            resourceType.Clear();
            resourceName.Clear();
            resourceFormat.Clear();
            resourceConverter.Clear();
            goto outside;
        }

        public static implicit operator BoundExpression(string expression)
        {
            return Parse(expression);
        }
    }
}
