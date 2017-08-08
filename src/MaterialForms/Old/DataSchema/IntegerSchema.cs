using System;
using System.Windows.Controls;
using MaterialForms.Controls;
using MaterialForms.ValueConverters;

namespace MaterialForms
{
    public class IntegerSchema : SchemaBase
    {
        private int? value;

        public int? Value
        {
            get { return value; }
            set
            {
                if (value == this.value) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public int MinValue { get; set; } = int.MinValue;

        public int MaxValue { get; set; } = int.MaxValue;

        public bool Required = true;

        public string LessThanMinimumMessage { get; set; } = "Value must be greater than or equal to {0}.";

        public string GreaterThanMaximumMessage { get; set; } = "Value must be less than or equal to {0}.";

        public string RequiredMessage { get; set; } = "Field is required.";

        public override UserControl CreateView()
        {
            return new SingleLineTextControl(new StringToIntegerConverter())
            {
                DataContext = this
            };
        }

        public override bool HoldsValue => true;

        public override object GetValue() => Value;

        public override void SetValue(object obj)
        {
            Value = obj as int?;
        }

        protected override bool OnValidation()
        {
            if (value == null)
            {
                Error = RequiredMessage;
                return false;
            }

            var val = value.Value;
            if (val < MinValue)
            {
                Error = string.Format(LessThanMinimumMessage, MinValue);
                return false;
            }

            if (val > MaxValue)
            {
                Error = string.Format(GreaterThanMaximumMessage, MaxValue);
                return false;
            }

            return true;
        }
    }
}
