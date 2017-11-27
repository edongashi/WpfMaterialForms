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
        ///     Gets or sets the value that indicates which form element should receive focus
        ///     when the form is displayed. A negative value disables this feature.
        /// </summary>
        public int FocusedSchema { get; set; } = 0;

        /// <summary>
        ///     Gets or sets the value of the schema with the specified key.
        /// </summary>
        public object this[string key]
        {
            get => GetSchema(key)?.GetValue();
            set => GetSchema(key)?.SetValue(value);
        }

        /// <summary>
        ///     Gets a new view bound to the current state of the object. Use this if you need to host your forms manually.
        /// </summary>
        public UserControl View => new FormView {DataContext = this};

        /// <summary>
        ///     Gets all form key-value pairs as a dictionary. Schemas that have no key or cannot hold values are excluded.
        /// </summary>
        public Dictionary<string, object> GetValuesDictionary()
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var schema in ValidKeySchemas())
                dictionary[schema.Key] = schema.GetValue();

            return dictionary;
        }

        /// <summary>
        ///     Gets all form name-value pairs as a dictionary. Schemas that have no name or cannot hold values are excluded.
        /// </summary>
        public Dictionary<string, object> GetValuesDictionaryFromNames()
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var schema in this.Where(schema => schema.HoldsValue && !string.IsNullOrEmpty(schema.Name)))
                dictionary[schema.Name] = schema.GetValue();

            return dictionary;
        }

        /// <summary>
        ///     Gets all form values as an indexed list. Schemas that cannot hold values are excluded.
        /// </summary>
        /// <remarks>Because some schemas cannot hold values, returned indexes may not match one to one with the form's.</remarks>
        public List<object> GetValuesList()
        {
            return (from schema in this where schema.HoldsValue select schema.GetValue()).ToList();
        }

        /// <summary>
        ///     Gets all form key-value pairs as a dynamic object with keys as property names. Schemas that have no key or cannot
        ///     hold values are excluded.
        /// </summary>
        public dynamic GetValuesDynamic()
        {
            var dictionary = (IDictionary<string, object>) new ExpandoObject();
            foreach (var schema in ValidKeySchemas())
                dictionary[schema.Key] = schema.GetValue();

            return dictionary;
        }

        /// <summary>
        ///     Creates a new instance of specified type and tries to assign all matching form values to it. Schemas that have no
        ///     keys or cannot hold values are excluded.
        /// </summary>
        /// <typeparam name="T">Type of object to be assigned to. Must have a default constructor.</typeparam>
        public T Bind<T>() where T : class, new()
        {
            return Bind(new T());
        }

        /// <summary>
        ///     Tries to assign all matching form values to an object of specified type. Schemas that have no keys or cannot hold
        ///     values are excluded.
        /// </summary>
        /// <typeparam name="T">Type of object to be assigned to.</typeparam>
        public T Bind<T>(T instance) where T : class
        {
            var type = instance.GetType();
            foreach (var schema in ValidKeySchemas())
                try
                {
                    var propertyInfo = type.GetProperty(schema.Key, BindingFlags.Instance | BindingFlags.Public);
                    propertyInfo?.SetValue(instance, schema.GetValue());
                }
                catch
                {
                    // ignored
                }

            return instance;
        }

        public bool Validate()
        {
            return this.All(schema => schema.Validate());
        }

        public IEnumerable<SchemaBase> ValidKeySchemas()
        {
            return this.Where(schema => schema.HoldsValue && !string.IsNullOrEmpty(schema.Key));
        }

        public SchemaBase GetSchema(string key)
        {
            return this.FirstOrDefault(schema => schema.Key == key);
        }
    }
}