using System;
using System.Collections.Generic;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;
using MaterialForms.Wpf.Validation;

namespace MaterialForms.Wpf.Fields
{
    /// <summary>
    /// Abstract base class for all input fields.
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
            Resources.Add("Value", new DataBinding(Key, BindingMode, Validators));
            Resources.Add("IsReadOnly", IsReadOnly ?? LiteralValue.False);
        }

        public IValueProvider IsReadOnly { get; set; }

        public BindingMode BindingMode { get; set; }

        public List<IValidatorProvider> Validators { get; set; }

        public abstract object GetDefaultValue();
    }
}
