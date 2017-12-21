using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MaterialForms.Mappers
{
    /// <summary>
    ///     The mapper class
    /// </summary>
    public class Mapper
    {
        /// <summary>
        ///     Global mapping overrides
        /// </summary>
        public static Dictionary<string, List<Mapper>> TypesOverrides { get; set; } =
            new Dictionary<string, List<Mapper>>();
        
        public static List<MaterialMapper> Mappers { get; set; }

        /// <summary>
        ///     This mapper attribute expression
        /// </summary>
        public Expression<Func<Attribute>>[] Expression { get; set; }

        /// <summary>
        ///     This mapper property info
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }

        /// <summary>
        ///     Call Include() in every IMapperClass
        /// </summary>
        public static void InitializeIMapperClasses()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(i =>
                i.IsClass && !i.ContainsGenericParameters && i.IsSubclassOf(typeof(MaterialMapper))).ToList();

            Mappers = types.Select(Activator.CreateInstance).OfType<MaterialMapper>().ToList();

            foreach (var type in Mappers)
                type.Include();
        }
    }
}