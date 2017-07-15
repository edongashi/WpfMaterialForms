using System;
using MaterialDesignThemes.Wpf;

namespace MaterialForms.Wpf.Annotations
{
    /// <summary>
    /// Indicates that the property is a form field and allows specifying its details.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FieldAttribute : Attribute
    {
        /// <summary>
        /// The display name of the field. Accepts a string or a dynamic expression.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tooltip of the field which shows on hover. Accepts a string or a dynamic expression.
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// The icon associated with the field. Not all field types may support icons.
        /// Accepts a <see cref="PackIconKind" /> or a dynamic resource.
        /// </summary>
        public object Icon { get; set; }

        /// <summary>
        /// Determines whether the field is editable. Accepts a boolean or a dynamic resource.
        /// </summary>
        public object IsReadOnly { get; set; }

        /// <summary>
        /// Determines the default value of this field. Accepts an object of the same type as the property type
        /// or a dynamic expression. Some types such as DateTime and numbers can be deserialized from strings.
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Determines the relative position of this field in the form.
        /// Fields are sorted based on this value, which has a default value of 0.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Specifies the row name. Fields sharing the same row name will be aligned in columns when possible.
        /// </summary>
        public string RowName { get; set; }

        /// <summary>
        /// Specifies the column number. Applicable only when <see cref="RowName"/> is set.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Specifies the column span. Applicable only when <see cref="RowName"/> is set.
        /// </summary>
        public int ColumnSpan { get; set; } = 1;
    }
}
