using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MaterialForms.Wpf.FormBuilding
{
    internal class PropertyInfoWrapper : IFormProperty
    {
        private readonly PropertyInfo property;

        public PropertyInfoWrapper(PropertyInfo property)
        {
            this.property = property;
        }

        public string Name => property.Name;

        public Type PropertyType => property.PropertyType;

        public Type DeclaringType => property.DeclaringType;

        public bool CanWrite => property.CanWrite && property.GetSetMethod(true).IsPublic;

        public T GetCustomAttribute<T>() where T : Attribute
        {
            return property.GetCustomAttribute<T>();
        }

        public IEnumerable<T> GetCustomAttributes<T>() where T : Attribute
        {
            return property.GetCustomAttributes<T>();
        }
    }
}
