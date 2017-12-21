using System.Collections.Generic;
using System.Windows;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public class StringField : DataFormField
    {
        public StringField(string key)
            : base(key, typeof(string))
        {
        }

        public IValueProvider IsMultiline { get; set; }

        protected internal override void Freeze()
        {
            base.Freeze();
            Resources.Add(nameof(IsMultiline), IsMultiline ?? LiteralValue.False);
        }

        protected internal override IBindingProvider CreateBindingProvider(IResourceContext context, IDictionary<string, IValueProvider> formResources)
        {
            return new StringPresenter(context, Resources, formResources);
        }
    }

    public class StringPresenter : ValueBindingProvider
    {
        static StringPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StringPresenter), new FrameworkPropertyMetadata(typeof(StringPresenter)));
        }

        public StringPresenter(IResourceContext context,
            IDictionary<string, IValueProvider> fieldResources,
            IDictionary<string, IValueProvider> formResources)
            : base(context, fieldResources, formResources, true)
        {
        }
    }
}
