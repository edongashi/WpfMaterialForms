using System;

namespace MaterialForms.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumDisplayAttribute : Attribute
    {
        public EnumDisplayAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
