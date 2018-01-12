using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MaterialForms.Wpf.Annotations;
using Proxier.Mappers;

namespace MaterialForms.Mappers
{
    public class MaterialMapper : AttributeMapper
    {
        protected MaterialMapper(Type type) : base(type)
        {
        }

        public bool AutoHide { get; set; }

        public override object TransfomSpawn(object createInstance)
        {
            if (AutoHide)
            {
                foreach (var type in Type.GetProperties().Except(Mappings.Select(i => i.PropertyInfo)))
                {
                    Mappings.Add(new Mapper(this)
                    {
                        Expression = new Expression<Func<Attribute>>[] {() => new FieldAttribute {IsVisible = false}},
                        PropertyInfo = type
                    });
                }
            }

            return base.TransfomSpawn(createInstance);
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