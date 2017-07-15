using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.FormBuilding
{
    public static class TypeUtilities
    {
        public static List<PropertyInfo> GetProperties(Type type, DefaultFields mode)
        {
            if (type == null)
            {
                throw new ArgumentException(nameof(type));
            }

            // First requirement is that properties and getters must be public.
            var properties = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.GetGetMethod(true).IsPublic);

            switch (mode)
            {
                case DefaultFields.AllIncludingReadonly:
                    return properties
                        .Where(p => p.GetCustomAttribute<FieldIgnoreAtribute>() == null)
                        .ToList();
                case DefaultFields.AllExcludingReadonly:
                    return properties.Where(p =>
                    {
                        if (p.GetCustomAttribute<FieldIgnoreAtribute>() != null)
                        {
                            return false;
                        }

                        if (p.GetCustomAttribute<FieldAttribute>() != null)
                        {
                            return true;
                        }

                        return p.CanWrite && p.GetSetMethod(true).IsPublic;
                    }).ToList();
                case DefaultFields.None:
                    return properties.Where(p =>
                    {
                        if (p.GetCustomAttribute<FieldIgnoreAtribute>() != null)
                        {
                            return false;
                        }

                        return p.GetCustomAttribute<FieldAttribute>() != null;
                    }).ToList();
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, "Invalid DefaultFields value.");
            }
        }
    }
}
