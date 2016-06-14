using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using MaterialForms.Views;

namespace MaterialForms
{
    public class MaterialForm : ObservableCollection<SchemaBase>
    {
        public Dictionary<string, object> GetValuesDictionary()
        {
            var dictionary = new Dictionary<string, object>();
            Action<string, object> assignFunction = (key, value) => dictionary[key] = value;
            AssignValues(assignFunction);
            return dictionary;
        }

        public List<object> GetValuesList()
        {
            return (from schema in this where schema.HoldsValue select schema.GetValue()).ToList();
        }

        public void AssignValues(Action<string, object> assignFunction)
        {
            foreach (var schema in this)
            {
                schema.AssignValue(assignFunction);
            }
        }

        public UserControl View => new FormView { DataContext = this };
    }
}
