using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf
{
    public abstract class FormField
    {
        /// <summary>
        /// Gets or sets the unique name of this field.
        /// </summary>
        public string Name { get; set; }

        public Resource IsVisible { get; set; }

        public BoundExpression Display { get; set; }

        public BoundExpression Hint { get; set; }
        
        public Resource Icon { get; set; }
    }

    public class StringField : FormField
    {
        public BoundExpression IsMultiline { get; set; }
    }
}
