using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MaterialForms.Mappers
{
    public class MaterialMapper
    {
        public MaterialMapper(Type type)
        {
            Type = type;
        }

        public MaterialMapper()
        {
        }

        public Type Type { get; set; }

        /// <inheritdoc />
        public List<Mapper> Mappings { get; } = new List<Mapper>();

        /// <inheritdoc />
        public void Include()
        {
            if (Type == null) return;

            var fullName = Type.FullName;
            if (fullName == null) return;

            if (!Mapper.TypesOverrides.ContainsKey(fullName))
                Mapper.TypesOverrides.Add(fullName, Mappings);
            else
                Mapper.TypesOverrides[fullName].AddRange(Mappings.Except(Mapper.TypesOverrides[fullName]));
        }
    }

    /// <summary>
    ///     Represents a class that can interact with the global mappings
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    public class MaterialMapper<TSource> : MaterialMapper
    {
        public MaterialMapper() : base(typeof(TSource))
        {
        }

        /// <summary>
        ///     Adds a mapper.
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <param name="propertyLambda"></param>
        public void AddMapper<TProperty>(Expression<Func<TSource, TProperty>> propertyLambda, params Expression<Func<Attribute>>[] expression)
        {
            var type = typeof(TSource);

            if (!(propertyLambda.Body is MemberExpression member))
                throw new ArgumentException(
                    $"Expression '{propertyLambda}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(
                    $"Expression '{propertyLambda}' refers to a field, not a property.");

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(
                    $"Expresion '{propertyLambda}' refers to a property that is not from type {type}.");

            var mapper = new Mapper
            {
                Expression = expression,
                PropertyInfo = propInfo
            };

            Mappings.Add(mapper);
        }
    }
}