using System;

namespace MaterialForms.Wpf.Annotations
{
    /// <summary>
    /// Specifies that a field uses a mask.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class MaskAttribute : Attribute
    {
        public MaskAttribute(string mask)
        {
            mask = Mask;
        }

        public string Mask { get; set; }
    }
}