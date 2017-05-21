using System;

namespace MaterialForms.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FieldRequiredAttribute : Attribute
    {
        public object IsRequired { get; set; }

        public string Message { get; set; }
    }
}
