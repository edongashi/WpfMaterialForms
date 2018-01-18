using System;
using System.Collections.Generic;
using System.Windows;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public class ProgressField : FormField
    {
        private readonly string key;

        public ProgressField(string key)
        {
            this.key = key;
        }

        public IValueProvider Maximum { get; set; }

        protected internal override void Freeze()
        {
            base.Freeze();
            Resources.Add("Value", new PropertyBinding(key, false));
            Resources.Add(nameof(Maximum), Maximum ?? new LiteralValue(100d));
        }

        protected internal override IBindingProvider CreateBindingProvider(IResourceContext context, IDictionary<string, IValueProvider> formResources)
        {
            return new ProgressPresenter(context, Resources, formResources);
        }
    }

    public class ProgressPresenter : ValueBindingProvider
    {
        static ProgressPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressPresenter), new FrameworkPropertyMetadata(typeof(ProgressPresenter)));
        }

        public ProgressPresenter(IResourceContext context,
            IDictionary<string, IValueProvider> fieldResources,
            IDictionary<string, IValueProvider> formResources)
            : base(context, fieldResources, formResources, true)
        {
        }
    }
}
