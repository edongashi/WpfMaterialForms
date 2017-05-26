using System;
using System.Collections.Generic;
using System.Windows;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf
{

    /// <summary>
    /// Default implementation of <see cref="IFieldValueProvider"/>.
    /// </summary>
    public class FieldValueProvider : IFieldValueProvider
    {
        public FieldValueProvider(FrameworkElement form,
            IDictionary<string, Resource> fieldResources,
            IDictionary<string, Resource> formResources)
        {
            Form = form;
            FieldResources = fieldResources;
            FormResources = formResources;
        }

        public FrameworkElement Form { get; }

        public IDictionary<string, Resource> FieldResources { get; }

        public IDictionary<string, Resource> FormResources { get; }

        public virtual object ProvideValue(string path)
        {
            if (FieldResources.TryGetValue(path, out var resource))
            {
                return resource.ProvideBinding(Form);
            }

            if (FormResources.TryGetValue(path, out resource))
            {
                return resource.ProvideBinding(Form);
            }

            throw new InvalidOperationException($"Resource {path} not found.");
        }
    }
}
