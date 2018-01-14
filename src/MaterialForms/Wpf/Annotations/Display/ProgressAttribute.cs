using System;

namespace MaterialForms.Wpf.Annotations.Display
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ProgressAttribute : Attribute
    {
        /// <summary>
        /// Progress maximum value. Accepts a numeric value or a dynamic resource.
        /// </summary>
        public object Maximum { get; set; }
    }
}
