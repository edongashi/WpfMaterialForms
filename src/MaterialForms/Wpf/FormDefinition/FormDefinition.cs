using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf
{
    public class FormDefinition
    {
        private bool frozen;

        public FormDefinition(Type modelType)
        {
            ModelType = modelType;
            Resources = new Dictionary<string, IValueProvider>();
            FormElements = new List<FormElement>();
        }

        public Type ModelType { get; }

        public IDictionary<string, IValueProvider> Resources { get; set; }
        
        public List<FormElement> FormElements { get; set; }

        public IValueProvider TitleMessage { get; set; }

        public IValueProvider DetailsMessage { get; set; }

        public IValueProvider CreateMessage { get; set; }

        public IValueProvider EditMessage { get; set; }

        public IValueProvider DeleteMessage { get; set; }

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
            frozen = true;
        }
    }

    public class ModelDefinition
    {
        
    }
}
