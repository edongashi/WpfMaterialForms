using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Ninject;
using Ninject.Infrastructure.Language;

namespace MaterialForms.Mappers
{
    public class ExtensionMapper
    {
        public MaterialMapper Mapper { get; set; }
        public Type BaseType { get; set; }

        public ExtensionMapper()
        {
        }

        public ExtensionMapper(MaterialMapper mapper)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            BaseType = Mapper.BaseType;
        }

        public object Spawn()
        {
            var objectToSpawn = Activator.CreateInstance(BaseType.GetInjectedType().AddParameterlessConstructor());
            return Mapper.TransfomSpawn(objectToSpawn);
        }
    }

    /// <summary>
    ///     The mapper class
    /// </summary>
    public class Mapper
    {
        public Mapper(MaterialMapper parent)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        /// <summary>
        ///     Global mapping overrides
        /// </summary>
        public static Dictionary<Type, ExtensionMapper> TypesOverrides { get; set; } =
            new Dictionary<Type, ExtensionMapper>();

        public static List<MaterialMapper> Mappers { get; set; }

        /// <summary>
        ///     This mapper attribute expression
        /// </summary>
        public Expression<Func<Attribute>>[] Expression { get; set; }

        /// <summary>
        ///     This mapper property info
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }

        public MaterialMapper Parent { get; set; }

        /// <summary>
        ///     Call Include() in every IMapperClass
        /// </summary>
        public static void InitializeIMapperClasses(IKernel kernel = null)
        {
            Mapper.TypesOverrides.Clear();

            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(i =>
                i.IsClass && !i.ContainsGenericParameters && i.IsSubclassOf(typeof(MaterialMapper))).ToList();

            Mappers = types.Select(i => kernel?.Get(i) ?? Activator.CreateInstance(i)).OfType<MaterialMapper>()
                .ToList();

            foreach (var type in Mappers)
            {
                if (kernel != null)
                {
                    type.Kernel = kernel;
                    type.OnKernelLoaded();
                }

                type.Include();
                TypesOverrides.Add(type.BaseType, new ExtensionMapper(type));
            }
        }
    }
}