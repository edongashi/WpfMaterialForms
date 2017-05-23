using System;

namespace MaterialForms.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class FieldValue : Attribute
    {
        public FieldValue(Must condition) 
            : this(condition, null, false)
        {
        }

        public FieldValue(Must condition, object value) 
            : this(condition, value, true)
        {
        }

        private FieldValue(Must condition, object value, bool hasValue)
        {
            Condition = condition;
            Value = value;
            HasValue = hasValue;
        }

        public Must Condition { get; }

        public object Value { get; }

        internal bool HasValue { get; }

        public string Message { get; set; }

        public string Converter { get; set; }

        public string IsEnforced { get; set; }
    }
}
