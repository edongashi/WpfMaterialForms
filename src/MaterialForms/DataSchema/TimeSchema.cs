using System;
using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class TimeSchema : SchemaBase
    {
        private string value;
        private bool is24Hours = true;

        public string Value
        {
            get { return value; }
            set
            {
                if (value == this.value) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public bool Is24Hours
        {
            get { return is24Hours; }
            set
            {
                if (value == is24Hours) return;
                is24Hours = value;
                OnPropertyChanged();
            }
        }

        public override UserControl CreateView()
        {
            return new TimePickerControl
            {
                DataContext = this
            };
        }

        public override bool HoldsValue => true;

        public override object GetValue() => Value;
    }
}
