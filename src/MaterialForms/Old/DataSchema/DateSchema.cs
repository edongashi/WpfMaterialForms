using System;
using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class DateSchema : SchemaBase
    {
        private DateTime? value;

        public DateTime? Value
        {
            get => value;
            set
            {
                if (value.Equals(this.value)) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public override bool HoldsValue => true;

        public override UserControl CreateView()
        {
            return new DatePickerControl
            {
                DataContext = this
            };
        }

        public override object GetValue()
        {
            return Value;
        }

        public override void SetValue(object obj)
        {
            Value = obj as DateTime?;
        }
    }
}