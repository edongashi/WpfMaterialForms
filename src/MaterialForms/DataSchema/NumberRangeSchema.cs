using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class NumberRangeSchema : SchemaBase
    {
        private int value;
        private int minValue;
        private int maxValue;

        public int Value
        {
            get { return value; }
            set
            {
                if (value == this.value) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public int MinValue
        {
            get { return minValue; }
            set
            {
                if (value == minValue) return;
                minValue = value;
                OnPropertyChanged();
            }
        }

        public int MaxValue
        {
            get { return maxValue; }
            set
            {
                if (value == maxValue) return;
                maxValue = value;
                OnPropertyChanged();
            }
        }

        public override UserControl CreateView()
        {
            return new SliderControl
            {
                DataContext = this
            };
        }

        public override bool HoldsValue => true;

        public override object GetValue() => Value;

        public override void SetValue(object obj)
        {
            if (obj is int)
            {
                Value = (int)obj;
            }
            else if (obj is string)
            {
                int result;
                int.TryParse((string)obj, out result);
                Value = result;
            }
        }
    }
}
