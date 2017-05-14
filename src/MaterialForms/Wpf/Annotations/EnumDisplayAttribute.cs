using System;

namespace MaterialForms.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumDisplayAttribute : Attribute
    {
        public EnumDisplayAttribute(string expression)
        {
            Expression = expression;
        }

        public string Expression { get; set; }
    }
}