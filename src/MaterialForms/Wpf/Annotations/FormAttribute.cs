using System;

namespace MaterialForms.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FormAttribute : Attribute
    {
        public FormAttribute(FieldGeneration fieldGeneration)
        {
            FieldGeneration = fieldGeneration;
        }

        public FieldGeneration FieldGeneration { get; }
    }

    public enum FieldGeneration
    {
        OptOut,
        OptIn
    }
}
