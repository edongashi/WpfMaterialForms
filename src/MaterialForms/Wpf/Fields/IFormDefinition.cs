using System;
using System.Collections.Generic;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields
{
    public interface IFormDefinition
    {
        IReadOnlyList<FormRow> FormRows { get; }

        double[] Grid { get; set; }

        Type ModelType { get; }

        IDictionary<string, IValueProvider> Resources { get; }

        object CreateInstance(IResourceContext context);
    }
}