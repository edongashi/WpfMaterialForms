using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialForms.Wpf
{
    public static class TypeManager
    {
        private static Dictionary<Type, MaterialFormSchema> CachedDefinitions;

        public static void RegisterFormDefinition(Type type, MaterialFormSchema schema)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (schema == null)
            {
                throw new ArgumentNullException(nameof(schema));
            }

            CachedDefinitions[type] = schema;
        }

        public static MaterialFormSchema GetDefinition(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            MaterialFormSchema schema;
            if (CachedDefinitions.TryGetValue(type, out schema))
            {
                return schema;
            }

            schema = BuildDefinition(type);
            CachedDefinitions[type] = schema;
            return schema;
        }

        public static bool IsSimpleType(Type type)
        {
            throw new NotImplementedException();
        }

        public static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static MaterialFormSchema BuildDefinition(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            throw new NotImplementedException();
        }
    }
}
