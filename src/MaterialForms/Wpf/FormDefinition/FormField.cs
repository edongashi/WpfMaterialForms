using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf
{
    public abstract class FormField
    {
        public string Name { get; set; }

        public BoundExpression Display { get; set; }

        public BoundExpression Hint { get; set; }
        
        public PackIconKind Icon { get; set; }
    }
}
