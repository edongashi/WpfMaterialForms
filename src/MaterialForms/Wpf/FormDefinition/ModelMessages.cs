using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf
{
    public class ModelMessages
    {
        /// <summary>
        /// Gets or sets the string expression that is displayed for a control's form.
        /// </summary>
        public BoundExpression Title { get; set; }

        public BoundExpression Details { get; set; }

        public BoundExpression Create { get; set; }

        public BoundExpression Edit { get; set; }

        public BoundExpression Delete { get; set; }
    }
}