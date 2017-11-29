using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class NumberRangeSchema : SchemaBase
    {
        private int maxValue;
        private int minValue;
        private int value;

        public int Value
        {
            get => value;
            set
            {
                if (value == this.value) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public int MinValue
        {
            get => minValue;
            set
            {
                if (value == minValue) return;
                minValue = value;
                OnPropertyChanged();
            }
        }

        public int MaxValue
        {
            get => maxValue;
            set
            {
                if (value == maxValue) return;
                maxValue = value;
                OnPropertyChanged();
            }
        }

        public override bool HoldsValue => true;

        public override UserControl CreateView()
        {
            return new SliderControl
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
            if (obj is int)
            {
                Value = (int) obj;
            }
            else if (obj is string)
            {
                int result;
                int.TryParse((string) obj, out result);
                Value = result;
            }
        }
    }
}