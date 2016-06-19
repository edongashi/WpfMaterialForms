using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using MaterialForms.Views;

namespace MaterialForms
{
    public class MaterialForm : ObservableCollection<SchemaBase>
    {
        public MaterialForm()
        {
        }

        public MaterialForm(params SchemaBase[] schemas)
            : base(schemas)
        {
        }

        /// <summary>
        /// Gets all form key-value pairs as a dictionary. Schemas that have no key or cannot hold values are excluded.
        /// </summary>
        public Dictionary<string, object> GetValuesDictionary()
        {
            var dictionary = new Dictionary<string, object>();
            Action<string, object> assignFunction = (key, value) => dictionary[key] = value;
            AssignValues(assignFunction);
            return dictionary;
        }

        /// <summary>
        /// Gets all form values as an indexed list. Schemas that cannot hold values are excluded.
        /// </summary>
        /// <remarks>Because some schemas cannot hold values, returned indexes may not match one to one with the form's.</remarks>
        public List<object> GetValuesList()
        {
            return (from schema in this where schema.HoldsValue select schema.GetValue()).ToList();
        }

        /// <summary>
        /// Gets all form key-value pairs as a dynamic object with keys as property names. Schemas that have no key or cannot hold values are excluded.
        /// </summary>
        public dynamic GetValuesDynamic()
        {
            var dictionary = (IDictionary<string, object>)new ExpandoObject();
            Action<string, object> assignFunction = (key, value) => dictionary[key] = value;
            AssignValues(assignFunction);
            return dictionary;
        }

        /// <summary>
        /// Creates a new instance of specified type and tries to assign all matching form values to it. Schemas that have no keys or cannot hold values are excluded.
        /// </summary>
        /// <typeparam name="T">Type of object to be assigned to. Must have a default constructor.</typeparam>
        public T Bind<T>() where T : class, new()
        {
            var instance = new T();
            Bind(instance);
            return instance;
        }

        /// <summary>
        /// Tries to assign all matching form values to an object of specified type. Schemas that have no keys or cannot hold values are excluded.
        /// </summary>
        /// <typeparam name="T">Type of object to be assigned to.</typeparam>
        public T Bind<T>(T instance) where T : class
        {
            var type = instance.GetType();
            Action<string, object> assignFunction = (key, value) =>
            {
                try
                {
                    var propertyInfo = type.GetProperty(key, BindingFlags.Instance | BindingFlags.Public);
                    propertyInfo?.SetValue(instance, value);
                }
                catch
                {
                    // ignored
                }
            };

            AssignValues(assignFunction);
            return instance;
        }

        /// <summary>
        /// Visits all valid form key-value pairs by invoking the provided function for each of them.
        /// </summary>
        public void AssignValues(Action<string, object> assignFunction)
        {
            foreach (var schema in this)
            {
                schema.AssignValue(assignFunction);
            }
        }

        /// <summary>
        /// Gets a new view bound to the current state of the object. Use this if you need to host your forms manually.
        /// </summary>
        public UserControl View => new FormView { DataContext = this };
    }
}
