﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public class BoundExpression : IValueProvider
    {
        public BoundExpression(string value)
        {
            StringFormat = value ?? throw new ArgumentNullException(nameof(value));
            IsPlainString = true;
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

            if (Resources.Count == 0)
            {
                IsPlainString = true;
            }
        }

        public string StringFormat { get; }

        public IReadOnlyList<Resource> Resources { get; }

        public bool IsPlainString { get; }

        public bool IsDynamic => Resources != null && Resources.Any(res => res.IsDynamic);

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

        public void SetValue(FrameworkElement container, DependencyObject element, DependencyProperty property)
        {
            if (Resources == null || Resources.Count == 0)
            {
                element.SetValue(property, StringFormat);
                return;
            }

            BindingOperations.SetBinding(element, property, ProvideBinding(container));
        }

        public BindingBase ProvideBinding(FrameworkElement container)
        {
            if (Resources == null || Resources.Count == 0)
            {
                return new LiteralValue(StringFormat).ProvideBinding(container);
            }

            if (Resources.Count == 1)
            {
                var resource = Resources[0];
                var binding = resource.ProvideBinding(container);
                if (StringFormat != null)
                {
                    binding.StringFormat = StringFormat;
                }

                return binding;
            }

            var multiBinding = new MultiBinding
            {
                StringFormat = StringFormat
            };

            foreach (var binding in Resources.Select(resource => resource.ProvideBinding(container)))
            {
                multiBinding.Bindings.Add(binding);
            }

            return multiBinding;
        }

        public object ProvideValue(FrameworkElement container)
        {
            if (Resources == null || Resources.Count == 0)
            {
                return StringFormat;
            }

            return ProvideBinding(container);
        }

        public static BoundExpression Parse(string expression)
        {
            return Parse(expression, contextualResource: null);
        }

        public static BoundExpression Parse(string expression, IDictionary<string, object> contextualResources)
        {
            Resource Factory(string name, bool oneTimeBinding, string converter)
            {
                if (!contextualResources.TryGetValue(name, out var value))
                {
                    var index = name.IndexOf('.');
                    var indexBracket = name.IndexOf('[');
                    var increment = 1;
                    if (index == -1 || indexBracket != -1 && indexBracket < index)
                    {
                        increment = 0;
                        index = indexBracket;
                    }

                    if (index == -1)
                    {
                        return null;
                    }

                    var source = name.Substring(0, index);
                    if (!contextualResources.TryGetValue(source, out value))
                    {
                        return null;
                    }

                    var path = name.Substring(index + increment);
                    switch (value)
                    {
                        case BindingProxy proxy:
                            return new BindingProxyResource(proxy, path, oneTimeBinding, converter);
                        case StringProxy proxy:
                            return new StringProxyResource(proxy, path, oneTimeBinding, converter);
                        case Lazy<BindingProxy> lazyProxy:
                            return new DeferredBindingProxyResource(e => lazyProxy.Value, path, oneTimeBinding, converter);
                        case Lazy<StringProxy> lazyProxy:
                            return new DeferredStringProxyResource(e => lazyProxy.Value, path, oneTimeBinding, converter);
                        case Func<FrameworkElement, BindingProxy> lazyProxy:
                            return new DeferredBindingProxyResource(lazyProxy, path, oneTimeBinding, converter);
                        case Func<FrameworkElement, StringProxy> lazyProxy:
                            return new DeferredStringProxyResource(lazyProxy, path, oneTimeBinding, converter);
                        case Resource _:
                            throw new InvalidOperationException("Cannot use nested paths for a resource.");
                        default:
                            return new BoundValue(value, path, oneTimeBinding, converter);
                    }
                }

                switch (value)
                {
                    case Resource resource:
                        return resource.Rewrap(converter);
                    case BindingProxy proxy:
                        return new BindingProxyResource(proxy, null, oneTimeBinding, converter);
                    case StringProxy proxy:
                        return new StringProxyResource(proxy, null, oneTimeBinding, converter);
                    case Lazy<BindingProxy> lazyProxy:
                        return new DeferredBindingProxyResource(e => lazyProxy.Value, null, oneTimeBinding, converter);
                    case Lazy<StringProxy> lazyProxy:
                        return new DeferredStringProxyResource(e => lazyProxy.Value, null, oneTimeBinding, converter);
                    case Func<FrameworkElement, BindingProxy> lazyProxy:
                        return new DeferredBindingProxyResource(lazyProxy, null, oneTimeBinding, converter);
                    case Func<FrameworkElement, StringProxy> lazyProxy:
                        return new DeferredStringProxyResource(lazyProxy, null, oneTimeBinding, converter);
                    default:
                        return new LiteralValue(value, converter);
                }
            }

            return Parse(expression, Factory);
        }

        public static BoundExpression Parse(string expression, Func<string, bool, string, Resource> contextualResource)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var i = 0;
            if (expression.StartsWith("\\"))
            {
                i = 1;
            }
            else if (expression.StartsWith("@"))
            {
                return new BoundExpression(expression.Substring(1));
            }

            var resources = new List<Resource>();
            var stringFormat = new StringBuilder();
            var resourceType = new StringBuilder();
            var resourceName = new StringBuilder();
            var resourceConverter = new StringBuilder();
            var resourceFormat = new StringBuilder();
            var oneTimeBind = false;
            var length = expression.Length;
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
            // A leading ^ indicates one time binding for contextual resources.
            if (expression[i] == '^')
            {
                if (++i == length)
                {
                    throw new FormatException("Unexpected end of input.");
                }

                oneTimeBind = true;
            }

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
            var converter = resourceConverter.ToString();
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
                    resource = contextualResource?.Invoke(resourceTypeString + key, oneTimeBind, converter);
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
            oneTimeBind = false;
            goto outside;
        }

        public static implicit operator BoundExpression(string expression)
        {
            return Parse(expression);
        }
    }
}