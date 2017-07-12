using System;

namespace MaterialForms.Wpf.Annotations
{
    /// <summary>
    /// Indicates how a form field should be treated.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class FieldTypeAttribute : Attribute
    {
    }
}
