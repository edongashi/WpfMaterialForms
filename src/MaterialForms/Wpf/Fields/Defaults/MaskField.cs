using System.Collections.Generic;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public class MaskField : DataFormField
    {
        public MaskField(string key) : base(key, typeof(string))
        {
        }

        public IValueProvider Mask { get; set; }

        protected internal override void Freeze()
        {
            base.Freeze();
            Resources.Add(nameof(Mask), Mask ?? LiteralValue.False);
        }

        protected internal override IBindingProvider CreateBindingProvider(IResourceContext context,
            IDictionary<string, IValueProvider> formResources)
        {
            return new StringPresenter(context, Resources, formResources);
        }
    }
}