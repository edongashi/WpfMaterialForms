using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;
using MaterialForms.Wpf.Validation;

namespace MaterialForms.Wpf.Fields
{
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
            Resources.Add("IsReadOnly", IsReadOnly ?? FalseValue);
        }

        public IValueProvider IsReadOnly { get; set; }

        public BindingMode BindingMode { get; set; }

        public List<IValidatorProvider> Validators { get; set; }
    }
}
