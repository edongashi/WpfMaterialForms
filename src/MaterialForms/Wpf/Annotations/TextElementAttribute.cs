using System;
using System.Runtime.CompilerServices;

namespace MaterialForms.Wpf.Annotations
{
    /// <summary>
    /// Represents textual content in a form.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public abstract class TextElementAttribute : Attribute
    {
        protected TextElementAttribute(string value, int position)
        {
            Position = position;
        }

        /// <summary>
        /// Element content. Accepts a string or a dynamic expression.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Determines the position relative to other text elements added to the attribute target.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Determines whether this element will be visible. Accepts a boolean or a dynamic resource.
        /// </summary>
        public object IsVisible { get; set; }

        /// <summary>
        /// If set to true and this attribute is attached to a property, this element will be displayed after the field.
        /// If set to true and this attribute is attached to a class, this element will be displayed after the form contents.
        /// </summary>
        public bool InsertAfter { get; set; }
    }

    /// <summary>
    /// Draws title text.
    /// </summary>
    public sealed class TitleAttribute : TextElementAttribute
    {
        public TitleAttribute(string value, [CallerLineNumber] int position = 0) : base(value, position)
        {
        }
    }

    /// <summary>
    /// Draws accented heading text.
    /// </summary>
    public sealed class HeadingAttribute : TextElementAttribute
    {
        public HeadingAttribute(string value, [CallerLineNumber] int position = 0) : base(value, position)
        {
        }
    }

    /// <summary>
    /// Draws regular text.
    /// </summary>
    public sealed class TextAttribute : TextElementAttribute
    {
        public TextAttribute(string value, [CallerLineNumber] int position = 0) : base(value, position)
        {
        }
    }
}
