using System;
using MaterialDesignThemes.Wpf;

namespace MaterialForms.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldAttribute : Attribute
    {
        public string Display { get; set; }

        public string Hint { get; set; }

        public PackIconKind Icon { get; set; }

        public bool ReadOnly { get; set; }

        public int Position { get; set; }
    }
}