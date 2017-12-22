using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MaterialForms.Wpf.Annotations;
using Ninject;

namespace MaterialForms.Mappers
{
    public class MaterialMapper
    {
        /// <summary>
        /// Automatically hides elements that are not defined already.
        /// </summary>
        public bool AutoHide { get; set; } = false;
        public IKernel Kernel { get; set; }
        public MaterialMapper Parent { get; set; }

        public MaterialMapper(Type type) : this()
        {
            BaseType = type;
            Type = type;
        }

        public MaterialMapper()
        {
            Parent = this;
        }

        internal virtual void HandleAction(object model, string action, object parameter)
        {
        }

        public Type Type { get; set; }
        public Type BaseType { get; }

        /// <inheritdoc />
        public List<Mapper> Mappings { get; } = new List<Mapper>();

        /// <inheritdoc />
        public void Include()
        {
            if (Type == null) return;
            if (!AutoHide) return;
            foreach (var propertyInfo in BaseType.GetProperties().Except(Mappings.Select(i => i.PropertyInfo)))
            {
                var mapper = new Mapper(this)
                {
                    Expression = new List<Expression<Func<Attribute>>>
                    {
                        () => new FieldAttribute {IsVisible = false}
                    }.ToArray(),
                    PropertyInfo = propertyInfo
                };
                Mappings.Add(mapper);
            }
        }

        public virtual object TransfomSpawn(object createInstance)
        {
            Kernel?.Inject(createInstance);
            return createInstance;
        }

        public virtual void OnKernelLoaded()
        {
           
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

        public virtual void Action(TSource model, string action, object parameter)
        {
        }

        internal override void HandleAction(object model, string action, object parameter)
        {
            Action((TSource) model.CopyTo(Activator.CreateInstance(Type.AddParameterlessConstructor())), action,
                parameter);
        }

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
    }
}