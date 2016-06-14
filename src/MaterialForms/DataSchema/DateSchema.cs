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
            get { return value; }
            set
            {
                if (value.Equals(this.value)) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public override UserControl CreateView()
        {
            return new DatePickerControl
            {
                DataContext = this
            };
        }

        public override bool HoldsValue => true;

        public override object GetValue() => Value;
    }
}
