using System;

namespace MaterialForms.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ValidatorAttribute : Attribute
    {
        public object Expression { get; set; }

        public object MustEqual { get; set; } = true;

        public string Message { get; set; }
    }
}
