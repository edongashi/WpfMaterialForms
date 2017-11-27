using System;

namespace MaterialForms.Wpf.Annotations
{
    /// <summary>
    ///     Specifies that a field uses a mask.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class MaskedAttribute : Attribute
    {
        public string Mask { get; set; }
    }
}