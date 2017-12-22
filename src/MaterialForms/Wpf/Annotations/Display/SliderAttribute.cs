using System;

namespace MaterialForms.Wpf.Annotations.Display
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SliderAttribute : Attribute
    {
        /// <summary>
        /// Slider minimum value. Accepts a numeric value or a dynamic resource.
        /// </summary>
        public object Minimum { get; set; }

        /// <summary>
        /// Slider maximum value. Accepts a numeric value or a dynamic resource.
        /// </summary>
        public object Maximum { get; set; }
    }
}
