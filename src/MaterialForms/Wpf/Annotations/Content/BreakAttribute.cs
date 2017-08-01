using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Fields.Defaults;
using MaterialForms.Wpf.FormBuilding;

namespace MaterialForms.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class BreakAttribute : FormContentAttribute
    {
        public BreakAttribute([CallerLineNumber] int position = 0) : base(position)
        {
        }

        /// <summary>
        /// Height of the break. Accepts a double or a dynamic resource.
        /// </summary>
        public object Height { get; set; } = 8d;

        protected override FormElement CreateElement(MemberInfo target)
        {
            return new BreakElement
            {
                Height = Utilities.GetResource<double>(Height, 8d, Deserializers.Double)
            };
        }
    }
}
