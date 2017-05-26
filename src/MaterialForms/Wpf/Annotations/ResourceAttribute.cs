using System;

namespace MaterialForms.Wpf.Annotations
{
    /// <summary>
    /// Allows attaching custom resources to fields or to the model.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class ResourceAttribute : Attribute
    {
        public ResourceAttribute(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public object Value { get; }
    }
}
