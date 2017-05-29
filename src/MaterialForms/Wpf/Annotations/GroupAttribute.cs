using System;

namespace MaterialForms.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GroupAttribute : Attribute
    {
        public GroupAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public string Display { get; set; }

        public int Position { get; set; }
    }
}
