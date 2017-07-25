using System.Windows;
using MaterialDesignThemes.Wpf;
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
        /// Gets or sets the string expression of the field's title.
        /// </summary>
        public IValueProvider Name { get; set; }

        /// <summary>
        /// Gets or sets the string expression of the field's tooltip.
        /// </summary>
        public IValueProvider ToolTip { get; set; }

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
            base.Freeze();
            Resources.Add(nameof(Name), Name ?? LiteralValue.Null);
            Resources.Add(nameof(ToolTip), ToolTip ?? LiteralValue.Null);
            var hasIcon = Icon != null && !(Icon is LiteralValue v && v.Value == null);
            Resources.Add(nameof(Icon), hasIcon ? Icon : new LiteralValue(default(PackIconKind)));
            Resources.Add("IconVisibility", new LiteralValue(hasIcon ? Visibility.Visible : Visibility.Collapsed));
        }
    }
}
