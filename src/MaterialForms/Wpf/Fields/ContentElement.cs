using System.Windows;
using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public abstract class ContentElement : FormElement
    {
        public IValueProvider Content { get; set; }

        public IValueProvider Icon { get; set; }

        public IValueProvider IconPadding { get; set; }

        protected internal override void Freeze()
        {
            base.Freeze();
            Resources.Add(nameof(Content), Content ?? LiteralValue.Null);
            Resources.Add(nameof(IconPadding), IconPadding ?? LiteralValue.False);
            var hasIcon = Icon != null && !(Icon is LiteralValue v && v.Value == null);
            Resources.Add(nameof(Icon), hasIcon ? Icon : new LiteralValue(default(PackIconKind)));
            Resources.Add("IconVisibility", new LiteralValue(hasIcon ? Visibility.Visible : Visibility.Collapsed));
        }
    }
}
