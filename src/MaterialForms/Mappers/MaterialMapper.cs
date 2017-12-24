using System;
using Proxier.Mappers;

namespace MaterialForms.Mappers
{
    public class MaterialMapper : AttributeMapper
    {
        protected MaterialMapper(Type type) : base(type)
        {
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