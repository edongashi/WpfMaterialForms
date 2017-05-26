using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields
{
    public abstract class DataFormField : FormField
    {
        protected DataFormField(string key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Validators = new List<ValidationRule>();
        }

        protected internal override void Freeze()
        {
            base.Freeze();
            Resources.Add("Value", new DataBinding(Key, BindingMode, Validators));
            Resources.Add("IsReadOnly", IsReadOnly ?? FalseValue);
        }

        public IValueProvider IsReadOnly { get; set; }

        public BindingMode BindingMode { get; set; }

        public List<ValidationRule> Validators { get; set; }
    }
}