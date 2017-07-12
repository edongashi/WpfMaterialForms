using System;
using System.Windows.Controls;

namespace MaterialForms.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class ValueAttribute : Attribute
    {
        public ValueAttribute(string converter, Must condition)
            : this(converter, condition, null, false)
        {
        }

        public ValueAttribute(string converter, Must condition, object argument)
            : this(converter, condition, argument, true)
        {
        }

        public ValueAttribute(Must condition) 
            : this(null, condition, null, false)
        {
        }

        public ValueAttribute(Must condition, object argument) 
            : this(null, condition, argument, true)
        {
        }

        private ValueAttribute(string converter, Must condition, object argument, bool hasValue)
        {
            Converter = converter;
            Condition = condition;
            Argument = argument;
            HasValue = hasValue;
            ValidationStep = ValidationStep.RawProposedValue;
            ValidatesOnTargetUpdated = false;
        }

        public string Converter { get; }

        public Must Condition { get; }

        public object Argument { get; }

        internal bool HasValue { get; }

        public string Message { get; set; }

        public object When { get; set; }

        public ValidationStep ValidationStep { get; set; }

        public bool ValidatesOnTargetUpdated { get; set; }
    }
}
