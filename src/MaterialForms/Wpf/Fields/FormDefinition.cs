using System;
using System.Collections.Generic;
using System.Dynamic;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields
{
    public class FormDefinition
    {
        private bool frozen;

        public FormDefinition(Type modelType)
        {
            ModelType = modelType;
            Resources = new Dictionary<string, IValueProvider>();
            Grid = new[] { 1d };
            FormRows = new List<FormRow>();
        }

        public Type ModelType { get; }

        public IDictionary<string, IValueProvider> Resources { get; set; }

        public double[] Grid { get; set; }

        public List<FormRow> FormRows { get; set; }

        public object CreateInstance()
        {
            if (ModelType != null)
            {
                return Activator.CreateInstance(ModelType);
            }

            if (!frozen)
            {
                throw new InvalidOperationException("Cannot create dynamic models without freezing this object.");
            }

            var expando = new ExpandoObject();
            IDictionary<string, object> dictionary = expando;
            foreach (var dataField in FormElements.OfType<DataFormField>())
            {
                dictionary[dataField.Key] = dataField.GetDefaultValue();
            }

            return expando;
        }

        protected internal virtual void Freeze()
        {
            if (frozen)
            {
                return;
            }

            frozen = true;
        }
    }
}
