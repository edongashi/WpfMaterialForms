using System;
using MaterialDesignThemes.Wpf;

namespace MaterialForms.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldAttribute : Attribute
    {
        public string Name { get; set; }

        public string Tooltip { get; set; }

        public PackIconKind Icon { get; set; }
    }
}
