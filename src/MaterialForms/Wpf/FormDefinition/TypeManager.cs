using System;
using System.Collections.Generic;

namespace MaterialForms.Wpf
{
    public static class TypeManager
    {
        private static Dictionary<Type, FormDefinition> CachedDefinitions;

        public static void RegisterFormDefinition(Type type, FormDefinition formDefinition)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (formDefinition == null)
            {
                throw new ArgumentNullException(nameof(formDefinition));
            }

            CachedDefinitions[type] = formDefinition;
        }

        public static FormDefinition GetDefinition(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            FormDefinition formDefinition;
            if (CachedDefinitions.TryGetValue(type, out formDefinition))
            {
                return formDefinition;
            }

            formDefinition = BuildDefinition(type);
            CachedDefinitions[type] = formDefinition;
            return formDefinition;
        }

        public static bool IsSimpleType(Type type)
        {
            throw new NotImplementedException();
        }

        public static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static FormDefinition BuildDefinition(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            throw new NotImplementedException();
        }
    }
}
