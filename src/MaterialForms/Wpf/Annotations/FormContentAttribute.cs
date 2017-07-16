using System;
using System.Reflection;
using MaterialForms.Wpf.Fields;

namespace MaterialForms.Wpf.Annotations
{
    /// <summary>
    /// Represents content attached before or after form elements.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public abstract class FormContentAttribute : Attribute
    {
        protected FormContentAttribute(int position)
        {
            Position = position;
        }

        /// <summary>
        /// Determines the position relative to other elements added to the attribute target.
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

        /// <summary>
        /// Create a form element corresponding to this object.
        /// </summary>
        /// <returns></returns>
        public abstract FormElement CreateElement(MemberInfo target);
    }
}