using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;
using MaterialForms.Wpf.Validation;

namespace MaterialForms.Wpf.Fields
{
    /// <summary>
    /// Base class for all form field definitions.
    /// </summary>
    public abstract class FormField
    {
        protected FormField()
        {
            Resources = new Dictionary<string, IValueProvider>();
        }

        /// <summary>
        /// Gets or sets the unique name of this field.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the bool resource that determines whether this field will be visible.
        /// </summary>
        public Resource IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the string expression of the field's title.
        /// </summary>
        public IValueProvider Display { get; set; }

        /// <summary>
        /// Gets or sets the string expression of the field's tooltip.
        /// </summary>
        public IValueProvider Hint { get; set; }

        /// <summary>
        /// Gets or sets the field's PackIconKind resource. Not all controls may display an icon.
        /// </summary>
        public IValueProvider Icon { get; set; }

        /// <summary>
        /// Finalizes the field state by adding all appropriate values as resources.
        /// Changing properties is strongly discouraged after this method has been called.
        /// </summary>
        protected internal virtual void Freeze()
        {
        }

        public IDictionary<string, IValueProvider> Resources { get; set; }

        protected abstract IFieldValueProvider CreateValueProvider(FrameworkElement form,
            IDictionary<string, IValueProvider> formResources);
    }

    public abstract class DataFormField : FormField
    {
        protected DataFormField(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Validators = new List<FieldValidator>();
        }

        protected internal override void Freeze()
        {
            Resources.Add("Value", new PropertyBinding(Name, BindingMode));
        }

        public BindingMode BindingMode { get; set; }

        public List<FieldValidator> Validators { get; set; }
    }
}
