using System;
using System.Collections.Generic;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;
using MaterialForms.Wpf.Validation;

namespace MaterialForms.Wpf.Fields
{
    /// <summary>
    /// Base class for all input fields.
    /// </summary>
    public abstract class DataFormField : FormField
    {
        protected DataFormField(string key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Validators = new List<IValidatorProvider>();
        }

        protected internal override void Freeze()
        {
            base.Freeze();
            if (CreateBinding)
            {
                if (IsDirectBinding)
                {
                    Resources.Add("Value", new DirectBinding(BindingMode, Validators));
                }
                else
                {
                    Resources.Add("Value", new DataBinding(Key, BindingMode, Validators));
                }
            }

            Resources.Add(nameof(IsReadOnly), IsReadOnly ?? LiteralValue.False);
            Resources.Add(nameof(DefaultValue), DefaultValue ?? new LiteralValue(null));
        }

        public IValueProvider IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets the default value for this field.
        /// </summary>
        public IValueProvider DefaultValue { get; set; }

        public BindingMode BindingMode { get; set; }

        public List<IValidatorProvider> Validators { get; set; }

        protected bool IsDirectBinding { get; set; }

        protected bool CreateBinding { get; set; } = true;

        public virtual object GetDefaultValue(IResourceContext context)
        {
            return DefaultValue?.GetValue(context).Value;
        }
    }
}
