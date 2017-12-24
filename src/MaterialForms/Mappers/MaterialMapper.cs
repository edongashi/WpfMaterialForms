using System;
using System.Linq.Expressions;
using System.Reflection;
using Proxier.Mappers;

namespace MaterialForms.Mappers
{
    public class MaterialMapper : AttributeMapper
    {
        protected MaterialMapper(Type type) : base(type)
        {
        }

        /// <summary>
        ///     Add an attribute to a class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public void AddClassAttribute(params Expression<Func<Attribute>>[] expression)
        {
            var mapper = new Mapper(this)
            {
                PropertyInfo = null,
                Expression = expression
            };

            Mappings.Add(mapper);
        }

        /// <summary>
        ///     Adds a mapper by name.
        /// </summary>
        public void AddProperty(string prop, Type type)
        {
            Type = Type.InjectProperty(prop, type);
        }

        /// <summary>
        ///     Adds a mapper by name.
        /// </summary>
        public void AddPropertyAttribute(string prop,
            params Expression<Func<Attribute>>[] expression)
        {
            Mappings.Add(new Mapper(this)
            {
                Expression = expression,
                PropertyInfo = Type.GetHighestProperty(prop)
            });
        }

        public virtual void HandleAction(object model, string action, object parameter)
        {
        }
    }

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
        public void AddPropertyAttribute<TProperty>(Expression<Func<TSource, TProperty>> propertyLambda,
            params Expression<Func<Attribute>>[] expression)
        {
            var type = Type;

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

            var mapper = new Mapper(this)
            {
                Expression = expression,
                PropertyInfo = propInfo
            };

            Mappings.Add(mapper);
        }

        /// <summary>
        ///     Called when [action happened]
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="action">The action.</param>
        /// <param name="parameter">The parameter.</param>
        public virtual void Action(TSource model, string action, object parameter)
        {
        }

        /// <param name="model">The model.</param>
        /// <param name="action">The action.</param>
        /// <param name="parameter">The parameter.</param>
        public override void HandleAction(object model, string action, object parameter)
        {
            Action((TSource) model.CopyTo(Activator.CreateInstance(Type.AddParameterlessConstructor())), action,
                parameter);
        }
    }
}