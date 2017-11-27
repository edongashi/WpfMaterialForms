using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class TimeSchema : SchemaBase
    {
        private bool is24Hours = true;
        private string value;

        public string Value
        {
            get => value;
            set
            {
                if (value == this.value) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public bool Is24Hours
        {
            get => is24Hours;
            set
            {
                if (value == is24Hours) return;
                is24Hours = value;
                OnPropertyChanged();
            }
        }

        public override bool HoldsValue => true;

        public override UserControl CreateView()
        {
            return new TimePickerControl
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
            Value = obj?.ToString();
        }
    }
}