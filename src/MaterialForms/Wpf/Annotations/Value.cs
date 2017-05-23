using System;

namespace MaterialForms.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class Value : Attribute
    {
        public Value(string converter, Must condition)
            : this(converter, condition, null, false)
        {
        }

        public Value(string converter, Must condition, object argument)
            : this(converter, condition, argument, true)
        {
        }

        public Value(Must condition) 
            : this(null, condition, null, false)
        {
        }

        public Value(Must condition, object argument) 
            : this(null, condition, argument, true)
        {
        }

        private Value(string converter, Must condition, object argument, bool hasValue)
        {
            Converter = converter;
            Condition = condition;
            Argument = argument;
            HasValue = hasValue;
        }

        public string Converter { get; }

        public Must Condition { get; }

        public object Argument { get; }

        internal bool HasValue { get; }

        public string Message { get; set; }

        public string IsEnforced { get; set; }
    }
}
