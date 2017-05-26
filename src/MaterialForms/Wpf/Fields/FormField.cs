using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields
{
    /// <summary>
    /// Base class for all form field definitions.
    /// </summary>
    public abstract class FormField : FormElement
    {
        /// <summary>
        /// Gets or sets the unique name of this field.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the bool resource that determines whether this field will be visible.
        /// </summary>
        public IValueProvider IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the string expression of the field's title.
        /// </summary>
        public IValueProvider Name { get; set; }

        /// <summary>
        /// Gets or sets the string expression of the field's tooltip.
        /// </summary>
        public IValueProvider Hint { get; set; }

        /// <summary>
        /// Gets or sets the field's PackIconKind resource. Not all controls may display an icon.
        /// </summary>
        public IValueProvider Icon { get; set; }

        /// <summary>
        /// Finalizes the field state by adding all appropriate values as resources.
        /// Changing properties after this method has been called is strongly discouraged.
        /// </summary>
        protected internal override void Freeze()
        {
            Resources.Add(nameof(IsVisible), IsVisible ?? FalseValue);
            Resources.Add(nameof(Name), Name ?? NullValue);
            Resources.Add(nameof(Hint), Hint ?? NullValue);
            Resources.Add(nameof(Icon), Icon ?? NullValue);
        }
    }
}
