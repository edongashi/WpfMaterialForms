using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

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
            if (Resources.Count == 0)
            {
                throw new ArgumentException("At least one resource reference is required.");
            }

            StringFormat = stringFormat ?? throw new ArgumentNullException(nameof(stringFormat));
        }

        public string StringFormat { get; }

        public List<Resource> Resources { get; }

        public void SetValue(FrameworkElement container, FrameworkElement element, DependencyProperty property)
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

        public static BoundExpression Parse(string expression)
        {
            throw new NotImplementedException();
        }

        public static implicit operator BoundExpression(string expression)
        {
            return Parse(expression);
        }
    }
}
