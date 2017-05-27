using System;
using System.Collections.Generic;
using System.Windows;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf
{
    /// <summary>
    /// Default implementation of <see cref="IBindingProvider"/>.
    /// </summary>
    public class BindingProvider : IBindingProvider
    {
        public BindingProvider(FrameworkElement form,
            IDictionary<string, IValueProvider> fieldResources,
            IDictionary<string, IValueProvider> formResources)
        {
            Form = form;
            FieldResources = fieldResources;
            FormResources = formResources;
        }

        public FrameworkElement Form { get; }

        public IDictionary<string, IValueProvider> FieldResources { get; }

        public IDictionary<string, IValueProvider> FormResources { get; }

        public virtual object ProvideValue(string path)
        {
            if (FieldResources.TryGetValue(path, out var resource))
            {
                return resource.ProvideValue(Form);
            }

            if (FormResources.TryGetValue(path, out resource))
            {
                return resource.ProvideValue(Form);
            }

            throw new InvalidOperationException($"Resource {path} not found.");
        }
    }
}
