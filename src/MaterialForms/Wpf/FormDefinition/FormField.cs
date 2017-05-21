using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf
{
    /// <summary>
    /// Base class for all form field definitions.
    /// </summary>
    public abstract class FormField
    {
        /// <summary>
        /// Gets or sets the unique name of this field.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the bool resource that determines whether this field will be visible.
        /// </summary>
        public Resource IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the string expression of the field's title.
        /// </summary>
        public BoundExpression Display { get; set; }

        /// <summary>
        /// Gets or sets the string expression of the field's tooltip.
        /// </summary>
        public BoundExpression Hint { get; set; }

        /// <summary>
        /// Gets or sets the field's PackIconKind resource. Not all controls may display an icon.
        /// </summary>
        public Resource Icon { get; set; }
    }
}
