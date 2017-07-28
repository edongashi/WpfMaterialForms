using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public abstract class ContentElement : FormElement
    {
        public IValueProvider Content { get; set; }

        protected internal override void Freeze()
        {
            base.Freeze();
            Resources.Add(nameof(Content), Content ?? LiteralValue.Null);
        }
    }
}