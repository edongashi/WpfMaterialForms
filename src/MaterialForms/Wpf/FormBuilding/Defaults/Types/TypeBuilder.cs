using System;
using System.Reflection;
using MaterialForms.Wpf.Fields;

namespace MaterialForms.Wpf.FormBuilding.Defaults.Types
{
    public abstract class TypeBuilder<T> : IFieldBuilder
    {
        public FormElement TryBuild(PropertyInfo property, Func<string, object> deserializer)
        {
            if (property.PropertyType != typeof(T))
            {
                return null;
            }

            return Build(property, deserializer);
        }

        protected abstract FormElement Build(PropertyInfo property, Func<string, object> deserializer);
    }
}
