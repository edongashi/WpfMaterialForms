using System.Collections.Generic;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public class StringField : DataFormField
    {
        public StringField(string key, string mask) : base(key, typeof(string))
        {
            Mask = new LiteralValue(mask);
        }

        public StringField(string key) : base(key, typeof(string))
        {
        }

        public IValueProvider IsMultiline { get; set; }
        public IValueProvider Mask { get; set; }

        protected internal override void Freeze()
        {
            base.Freeze();
            Resources.Add(nameof(IsMultiline), IsMultiline ?? LiteralValue.False);
            Resources.Add(nameof(Mask), Mask ?? LiteralValue.Null);
        }

        protected internal override IBindingProvider CreateBindingProvider(IResourceContext context,
            IDictionary<string, IValueProvider> formResources)
        {
            return new StringPresenter(context, Resources, formResources);
        }
    }
}