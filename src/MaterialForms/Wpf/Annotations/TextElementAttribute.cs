using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using MaterialForms.Wpf.Fields;

namespace MaterialForms.Wpf.Annotations
{
    /// <summary>
    /// Represents textual content in a form.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public abstract class TextElementAttribute : FormContentAttribute
    {
        protected TextElementAttribute(string value, int position) : base(position)
        {
            Value = value;
        }

        /// <summary>
        /// Element content. Accepts a string or a dynamic expression.
        /// </summary>
        public string Value { get; }
    }

    /// <summary>
    /// Draws title text.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class TitleAttribute : TextElementAttribute
    {
        public TitleAttribute(string value, [CallerLineNumber] int position = 0) : base(value, position)
        {
        }

        public override FormElement CreateElement(MemberInfo target)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Draws accented heading text.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class HeadingAttribute : TextElementAttribute
    {
        public HeadingAttribute(string value, [CallerLineNumber] int position = 0) : base(value, position)
        {
        }

        public override FormElement CreateElement(MemberInfo target)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Draws regular text.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class TextAttribute : TextElementAttribute
    {
        public TextAttribute(string value, [CallerLineNumber] int position = 0) : base(value, position)
        {
        }

        public override FormElement CreateElement(MemberInfo target)
        {
            throw new NotImplementedException();
        }
    }
}
